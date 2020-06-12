using System.Xml.Linq;
using System.Collections.Concurrent;
using PaDSolver.Model.BoardScoreEval;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PaDSolver.Model.Pattern;

namespace PaDSolver.Model.Solver
{
    class LinkSolver : ISolver
    {
        const int BannedLastTraveledPointCount=3;
        int Attempts = 0;
        System.Timers.Timer timer = new System.Timers.Timer(1000);
        public int ThreadCount{get;set;}=1;
        
        public int AllowPathScoreDrop{get;set;}=0;
        string[] AvailableDir;
        Dictionary<string,string> BackwardDirDict;

        int TargetScore;
        public int ScoreDropSpeed {get;set;}=-150;

        public bool EnableScoreDrop {get;set;}=true;
        public async Task<Route> SolveBoard(Board board)
        {
            if (ThreadCount<1)
                ThreadCount=1;

            Console.WriteLine($"Solve board using {ThreadCount} threads...");

            var tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;

            AvailableDir = board.MoveDirection == 4 ?
                new string[] { "U", "D", "L", "R" } : new string[] { "U", "D", "L", "R", "LU", "LD", "RU", "RD" };
            BackwardDirDict = new Dictionary<string, string>()
            {
                {"D","U"},{"U","D"},{"L","R"},{"R","L"},
                {"RD","LU"},{"RU","LD"},{"LD","RU"},{"LU","RD"}
            };

            TargetScore=board.TargetScore;
            ScoreDropSpeed = Math.Min(ScoreDropSpeed,0);

            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
            
            //seperate points for multi thread
            var SeperatedStartPoints = new List<List<Point>>();
                for (int i = 0; i < ThreadCount; i++)
                    SeperatedStartPoints.Add(new List<Point>());
            int Idx=0;
            for (int x = board.SelectStartX; x < board.SelectEndX; x++)
                for (int y = board.SelectStartY; y < board.SelectEndY; y++)
                    SeperatedStartPoints[Idx++%ThreadCount].Add(new Point(x,y));

            List<Task<Route>> Tasks = new List<Task<Route>>();
            var StartTime = DateTime.Now;
            for (int i = 0; i < ThreadCount; i++)
            {
                var SelectedStartPoint=SeperatedStartPoints[i];
                Task<Route> t = new Task<Route>(()=>GenTask(board.Clone(),SelectedStartPoint, ct));
                Tasks.Add(t);
                t.Start();
            }

            await Task.WhenAny(Tasks);
            tokenSource.Cancel();
            timer.Enabled = false;

            foreach (var t in Tasks)
            {
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    var result = t.Result;
                    result.TimeComsumedMs = (int)(DateTime.Now - StartTime).TotalMilliseconds;
                    result.Iteration = Attempts;
                    return result;
                }
            }

            return null;
        }

        int LastAttemptsGet = 0;
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine($"{(Attempts- LastAttemptsGet) / (timer.Interval/1000)} tries per second");
            LastAttemptsGet = Attempts;
            if(EnableScoreDrop)
                Interlocked.Add(ref TargetScore,ScoreDropSpeed);
        }


        public Route GenTask(Board board,List<Point> StartPoints, CancellationToken ct)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            List<string> Directions = new List<string>();
            
            int CurrentScore = 0;
            Route Result = new Route();
            Board TempBoard = null;

            int PickStartIdx=0;

            try
            {
                do
                {
                    Interlocked.Increment(ref Attempts);

                    Directions.Clear();

                    Point CurrentPoint = StartPoints[PickStartIdx++];
                    if (PickStartIdx >= StartPoints.Count)
                        PickStartIdx=0;

                    Result.StartX = CurrentPoint.X;
                    Result.StartY = CurrentPoint.Y;

                    List<Point>LastTraveledPath = new List<Point>();
                    TempBoard = board.Clone();

                    IBoardEvaluator eval = new EvaluatorCollection {
                        new ComboEvaluator() { PerScore = 1000 },
                        new Hori2Pattern(){ PerScore=10},
                        new Vert2Pattern(){ PerScore=10},

                        // new Hori4Pattern(){ PerScore=-10},
                        // new Hori4Pattern(){ PerScore=-10},
                        // new Hori6Pattern(){ PerScore=-10},
                        // new Vert4Pattern(){ PerScore=-10},
                        // new Vert5Pattern(){ PerScore=-10},
                    };

                    for (int i = 0; i < board.StepLimit; i++)
                    {
                        ct.ThrowIfCancellationRequested();
                        Dictionary<string,int>DirToScore = new Dictionary<string, int>();
                        foreach (var dir in AvailableDir)
                        {
                            var DirBoard = TempBoard.Clone();
                            //Try all path select max score
                            var BackDir = BackwardDirDict[dir];
                            if (BackDir == Directions.LastOrDefault())
                                continue;//don't select backward path
                            Point NextPoint=DirBoard.MoveBeads(CurrentPoint, dir);
                            if (CurrentPoint == NextPoint)
                                continue;//MoveFail
                            if (LastTraveledPath.Contains(NextPoint))
                                continue;//prevent infinite loop

                            int DirScore = eval.EvalBoard(DirBoard);
                            DirToScore.Add(dir,DirScore);
                        }
                        //Select Max score direction
                        int MaxScore = DirToScore.Values.Max();
                        var AcceptableScores = DirToScore.Where(x => x.Value >= MaxScore-AllowPathScoreDrop);
                        var SelectScoreDirKvp = AcceptableScores.ElementAt(rand.Next(AcceptableScores.Count()));
                        var MaxScoreDir = SelectScoreDirKvp.Key;
                        CurrentScore=SelectScoreDirKvp.Value;

                        Directions.Add(MaxScoreDir);
                        CurrentPoint = TempBoard.MoveBeads(CurrentPoint,MaxScoreDir);
                        LastTraveledPath.Add(CurrentPoint);

                        while (LastTraveledPath.Count>BannedLastTraveledPointCount)
                            LastTraveledPath.RemoveAt(0);
                        //Console.WriteLine(TempBoard.Dump());

                        if (CurrentScore >= TargetScore)
                            break;
                    }

                } while (CurrentScore < TargetScore);
                Console.WriteLine($"Finish in {Attempts.ToString()}");
                Console.WriteLine($"TargetScore {TargetScore.ToString()},Final Score {CurrentScore.ToString()}");

                Result.Score = CurrentScore;
                Result.Result = TempBoard.Beads;
                Result.Directions = Directions;

                return Result;
            }
            catch (OperationCanceledException)
            {
                //Console.WriteLine($"Cancelled Task Id:{Thread.CurrentThread.ManagedThreadId}");
            }

            return null;
        }

    }
}

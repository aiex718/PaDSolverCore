using System.Collections.Concurrent;
using PaDSolver.Model.BoardScoreEval;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaDSolver.Model.Solver
{
    class RandomSolver : ISolver
    {
        int Attempts = 0;
        System.Timers.Timer timer = new System.Timers.Timer(1000);
        public int ThreadCount{get;set;}=1;

        string[] AvailableDir,BackwardDir;

        public bool EnableScoreDrop {get;set;}
        public int ScoreDropPerSec {get;set;}
        public int TargetScore;

        public async Task<Route> SolveBoard(Board board)
        {
            if (ThreadCount<1)
                ThreadCount=1;

            Console.WriteLine($"Solve board using {ThreadCount} threads...");

            var tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;

            AvailableDir = board.MoveDirection == 4 ?
                new string[] { "U", "D", "L", "R" } : new string[] { "U", "D", "L", "R", "LU", "LD", "RU", "RD" };
            BackwardDir = board.MoveDirection == 4 ?
                new string[] { "D", "U", "R", "L" } : new string[] { "D", "U", "R", "L", "RD", "RU", "LD", "LU" };
    
            TargetScore=board.TargetScore;
            ScoreDropPerSec = Math.Max(ScoreDropPerSec,0);

            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
            List<Task<Route>> Tasks = new List<Task<Route>>();

            var StartTime = DateTime.Now;
            for (int i = 0; i < ThreadCount; i++)
            {
                Task<Route> t = new Task<Route>(()=>GenTask(board.Clone(), ct));
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
                Interlocked.Add(ref TargetScore,ScoreDropPerSec*-1);
        }


        public Route GenTask(Board board, CancellationToken ct)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            List<string> Directions = new List<string>();

            int Score = 0;
            Route Result = new Route();
            Board TempBoard = null;

            IBoardEvaluator eval = new ComboEvaluator() { PerScore = 1000 };

            int PickX = board.SelectStartX;
            int PickY = board.SelectStartY;

            try
            {
                do
                {
                    Interlocked.Increment(ref Attempts);

                    Directions.Clear();

                    Point CurrentPoint = new Point(PickX, PickY);
                    if (++PickX >= board.SelectEndX)
                    {
                        PickX = 0;
                        if (++PickY >= board.SelectEndY)
                            PickY = 0;
                    }

                    Result.StartX = CurrentPoint.X;
                    Result.StartY = CurrentPoint.Y;

                    TempBoard = board.Clone();

                    for (int i = 0; i < board.StepLimit; i++)
                    {
                        ct.ThrowIfCancellationRequested();
                        int DirRnd = rand.Next(AvailableDir.Length);
                        var dir = AvailableDir[DirRnd];
                        var BackDir = BackwardDir[DirRnd];
                        if (BackDir == Directions.LastOrDefault())
                        {
                            --i;
                            continue;//don't select backward path
                        }

                        var NextPoint = TempBoard.MoveBeads(CurrentPoint, dir);
                        if (CurrentPoint == NextPoint)//MoveFail
                        {
                            --i;
                            continue;
                        }
                        CurrentPoint = NextPoint;

                        Directions.Add(dir);

                        Score = eval.EvalBoard(TempBoard.Clone());

                        if (Score >= TargetScore)
                            break;
                    }

                } while (Score < TargetScore);
                Console.WriteLine($"Finish in {Attempts.ToString()}");
                Console.WriteLine($"TargetScore {TargetScore.ToString()},Final Score {Score.ToString()}");


                Result.Score = Score;
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

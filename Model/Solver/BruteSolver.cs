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
    class BruteSolver : ISolver
    {
        int Attempts = 0;
        System.Timers.Timer timer = new System.Timers.Timer(1000);
        public int ThreadCount{get;set;}=1;

        //ConcurrentDictionary<Point,ConcurrentBag<string>> TriedPaths;

        public Route SolveBoard(Board board)
        {
            if (ThreadCount<1)
                ThreadCount=1;

            Console.WriteLine($"Solve board using {ThreadCount} threads...");

            var tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;

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

            Task.WhenAny(Tasks).Wait();
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
        }


        public Route GenTask(Board board, CancellationToken ct)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            List<string> Directions = new List<string>();

            string[] AvailableDir = board.MoveDirection == 4 ?
                new string[] { "U", "D", "L", "R" } : new string[] { "U", "D", "L", "R", "LU", "LD", "RU", "RD" };
            string[] BackwardDir = board.MoveDirection == 4 ?
                new string[] { "D", "U", "R", "L" } : new string[] { "D", "U", "R", "L", "RD", "RU", "LD", "LU" };

            int Score = 0;
            Route Result = new Route();
            Board TempBoard = null;

            int PickX = board.SelectStartX;
            int PickY = board.SelectStartY;

            try
            {
                do
                {
                    ct.ThrowIfCancellationRequested();
                    Interlocked.Increment(ref Attempts);

                    Directions.Clear();

                    Point StartPoint = new Point(PickX, PickY);
                    if (++PickX >= board.SelectEndX)
                    {
                        PickX = 0;
                        if (++PickY >= board.SelectEndY)
                            PickY = 0;
                    }

                    Result.StartX = StartPoint.X;
                    Result.StartY = StartPoint.Y;

                    TempBoard = board.Clone();

                    IBoardEvaluator eval = new ComboEval() { Score = 1000 };

                    for (int i = 0; i < board.StepLimit; i++)
                    {
                        int DirRnd = rand.Next(AvailableDir.Length);
                        var dir = AvailableDir[DirRnd];
                        var BackDir = BackwardDir[DirRnd];
                        if (BackDir == Directions.LastOrDefault())
                        {
                            --i;
                            continue;//don't select backward path
                        }

                        var NextPoint = TempBoard.MoveBeads(StartPoint, dir);
                        if (StartPoint == NextPoint)//MoveFail
                        {
                            --i;
                            continue;
                        }
                        StartPoint = NextPoint;

                        Directions.Add(dir);

                        Score = eval.EvalBoard(TempBoard.Clone());

                        if (Score >= board.TargetScore)
                            break;
                    }

                } while (Score < board.TargetScore);
                Console.WriteLine("Finish in " + Attempts.ToString());

                Result.Score = Score;
                Result.Result = TempBoard.Beads;
                Result.Directions = Directions;

                return Result;
            }
            catch (OperationCanceledException e)
            {
                //Console.WriteLine($"Cancelled Task Id:{Thread.CurrentThread.ManagedThreadId}");
            }

            return null;
        }

    }
}

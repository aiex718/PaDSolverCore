using PaDSolver.Core.Extensions;
using PaDSolver.Model;
using PaDSolver.Model.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver.Core
{
    

    class Benchmark
    {
        public class Result
        {
            public string SolverName {get;set;}
            public int RoundCount { get; set; }
            public double AvgScore { get; set; }
            public double AvgTimeMs { get; set; }
            public double AvgSteps { get; set; }
            public double AvgIterationPerRoute { get; set; }
            public double AvgIterationPerSecond { get; set; }           

            public void Print()
            {
                Console.WriteLine($"{SolverName} Benchmark {RoundCount} times finish");
                Console.WriteLine($"Avg Score {AvgScore}");
                Console.WriteLine($"Avg TimeMs usage {AvgTimeMs}");
                Console.WriteLine($"Avg Iteration per route {AvgIterationPerRoute}");
                Console.WriteLine($"Avg Steps per route {AvgSteps}");
                Console.WriteLine($"Avg Iteration per second {AvgIterationPerSecond}");
            }
        }

        public Random Rand {get;set;}
        public int ThreadCount { get; set; }
        public int RoundCount { get; set; }
        List<Route> Routes { get; set; }

        public int BeadTypes { get; set; }
        public int TargetScore { get; set; }

        public bool EnableScoreDrop { get; set; }
        public int ScoreDropPerSec { get; set; }

        SolverFactory Factory;

        public Benchmark(SolverFactory solverFactory)
        {
            Factory = solverFactory;
        }


        public async Task<Result> Start()
        {
            Routes = new List<Route>();
            for (int i = 0; i < RoundCount; i++)
            {
                //Gen Board
                Board b = new Board();
                b.Random(6, 5, BeadTypes,Rand);
                b.SelectStartX = 0;
                b.SelectEndX = b.Width;
                b.SelectStartY = 0;
                b.SelectEndY = b.Height;
                b.StepLimit = 40;
                b.MoveDirection = 4;
                b.TargetScore = TargetScore;

                //Console.WriteLine("Generated Board");
                //Console.WriteLine(b.ToString());
                //Console.WriteLine(b.Dump());

                var solver = Factory.GenSolver();
                solver.ThreadCount=this.ThreadCount;
                solver.EnableScoreDrop = this.EnableScoreDrop;
                solver.ScoreDropPerSec = this.ScoreDropPerSec;
                var route = await solver.SolveBoard(b);

                //Console.WriteLine(route.ToString());
                //Console.WriteLine(route.Result.Dump(b.Width));

                Routes.Add(route);
            }

            return new Result()
            {
                SolverName=Factory.solverName,
                RoundCount = this.RoundCount,
                AvgScore = Routes.Select(x=>x.Score).Average(),
                AvgTimeMs = Routes.Select(x => x.TimeComsumedMs).Average(),
                AvgIterationPerRoute= Routes.Select(x => x.Iteration).Average(),
                AvgSteps= Routes.Select(x => x.Directions.Count).Average(),
                AvgIterationPerSecond= Routes.Select(x => x.Iteration).Sum() / (Routes.Select(x => x.TimeComsumedMs).Sum() / 1000)
            };
        }

    }
}

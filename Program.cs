using PaDSolver.Core;
using PaDSolver.Core.Extensions;
using PaDSolver.Model;
using PaDSolver.Model.BoardScoreEval;
using PaDSolver.Model.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int rndSeed=Guid.NewGuid().GetHashCode();//12345678//Guid.NewGuid().GetHashCode();

            var BruteResult = await( new Benchmark( new SolverFactory(nameof(BruteSolver))){ 
                RoundCount=100,
                ThreadCount=6,
                Rand=new Random(rndSeed),
                TargetScore=6000
            }.Start());
            BruteResult.Print();

            var PatternResult = await( new Benchmark( new SolverFactory(nameof(PatternSolver))){ 
                RoundCount=100,
                ThreadCount=6,
                Rand=new Random(rndSeed),                
                TargetScore=6000
            }.Start());
            PatternResult.Print();
            
            // Board b = new Board();
            // b.Random(6, 5, 6);
            // b.SelectStartX = 0;
            // b.SelectEndX = b.Width;
            // b.SelectStartY = 0;
            // b.SelectEndY = b.Height;
            // b.StepLimit = 40;
            // b.MoveDirection = 4;
            // b.TargetScore = 6000;
            // b.Weights=new List<float>(){1,1,1,1,1,1};

            // // b.Beads = new List<int>()
            // // {
            // //     0,1,1,1,1,0,
            // //     1,1,1,3,4,5,
            // //     1,0,1,4,5,3,
            // //     0,1,2,3,4,3,
            // //     1,0,1,3,3,3,
            // // };

            // //var eval = new Vert3Pattern();
            // //Console.WriteLine(eval.EvalBoard(b));

            // Console.WriteLine("Generated Board");
            // Console.WriteLine(b.ToString());
            // Console.WriteLine(b.Dump());

            // var solver = new PatternSolver(){ThreadCount=6};
            // var route = await solver.SolveBoard(b);

            // Console.WriteLine(route.ToString());
            // Console.WriteLine(route.Result.Dump(b.Width));
        }
    }
}

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
            int rndSeed=12345678;//12345678//Guid.NewGuid().GetHashCode();

            // var BruteResult = await( new Benchmark( new SolverFactory(nameof(BruteSolver))){ 
            //     RoundCount=100,
            //     ThreadCount=6,
            //     Rand=new Random(rndSeed),
            //     TargetScore=6000
            // }.Start());
            // BruteResult.Print();

            var PatternResult = await( new Benchmark( new SolverFactory(nameof(LinkSolver))){ 
                RoundCount=100,
                ThreadCount=6,
                Rand=new Random(rndSeed),                
                TargetScore=6000
            }.Start());
            PatternResult.Print();
        }
    }
}

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
        static void Main(string[] args)
        {
            Benchmark benchmark = new Benchmark( new SolverFactory(nameof(BruteSolver))){ 
                RoundCount=100 ,
                ThreadCount=6,
                Rand=new Random(12345678),
            };

            var Result = benchmark.Start();
            Task.Delay(2000).Wait();
            Result.Print();

        }
    }
}

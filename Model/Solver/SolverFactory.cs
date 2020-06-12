using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver.Model.Solver
{
    class SolverFactory 
    {
        public string solverName {get; set;}
        public SolverFactory (string SolverName)
        {
            solverName = SolverName;
        }

        public ISolver GenSolver()
        {
            switch (solverName)
            {
                case(nameof(BruteSolver)):
                    return new BruteSolver();
                case(nameof(PatternSolver)):
                    return new PatternSolver();
            }

            return null;
        }
    }

}

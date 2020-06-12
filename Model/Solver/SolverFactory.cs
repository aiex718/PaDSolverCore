using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver.Model.Solver
{
    interface ISolverFactory
    {
        ISolver GenSolver();
    }


    class SolverFactory : ISolverFactory
    {
        string solverName;
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
            }

            return null;
        }
    }

}

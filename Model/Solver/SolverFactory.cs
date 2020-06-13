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
                case(nameof(RandomSolver)):
                    return new RandomSolver();
                case(nameof(Conn2Solver)):
                    return new Conn2Solver();
                case(nameof(LinkSolver)):
                    return new LinkSolver();
            }
            throw new KeyNotFoundException(solverName+" Not Found");
        }
    }

}

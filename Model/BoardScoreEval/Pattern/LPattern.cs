using PaDSolver.Model.BoardScoreEval;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver.Model.Pattern
{
    class LLUPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,1,1},
               {1,0,0},
               {1,0,0}
            };
    }

    class LLDPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,0,0},
               {1,0,0},
               {1,1,1}
            };
    }

    class LRUPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,1,1},
               {0,0,1},
               {0,0,1}
            };
    }

    class LRDPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {0,0,1},
               {0,0,1},
               {1,1,1}
            };
    }

}
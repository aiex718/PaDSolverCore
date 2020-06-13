using PaDSolver.Model.BoardScoreEval;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver.Model.Pattern
{
    class ArrowLPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {0,1},
               {1,0},
               {0,1}
            };
    }

    class ArrowRPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,0},
               {0,1},
               {1,0}
            };
    }

    class ArrowUPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {0,1,0},
               {1,0,1}
            };
    }

    class ArrowDPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,0,1},
               {0,1,0}
            };
    }

}
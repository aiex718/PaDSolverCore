using PaDSolver.Model.BoardScoreEval;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver.Model.Pattern
{
    class TUPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,1,1},
               {0,1,0},
               {0,1,0}
            };
    }

    class TDPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {0,1,0},
               {0,1,0},
               {1,1,1}
            };
    }

    class TLPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,0,0},
               {1,1,1},
               {1,0,0}
            };
    }

    class TRPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {0,0,1},
               {1,1,1},
               {0,0,1}
            };
    }

}
using PaDSolver.Model.BoardScoreEval;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver.Model.Pattern
{
    class BowLUPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {0,1},
               {1,0},
               {1,0}
            };
    }

    class BowRUPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,0},
               {0,1},
               {0,1}
            };
    }

    class BowLDPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,0},
               {1,0},
               {0,1}
            };
    }

    class BowRDPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {0,1},
               {0,1},
               {1,0}
            };
    }

    class BowULPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {0,1,1},
               {1,0,0}
            };
    }

    class BowURPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,1,0},
               {0,0,1}
            };
    }

    class BowDLPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,0,0},
               {0,1,1}
            };
    }

    class BowDRPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {0,0,1},
               {1,1,0}
            };
    }

}
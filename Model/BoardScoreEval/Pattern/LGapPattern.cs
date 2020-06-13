using PaDSolver.Model.BoardScoreEval;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver.Model.Pattern
{
    class LGapULPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,1},
               {0,0},
               {1,0}
            };
    }

    class LGapURPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,1},
               {0,0},
               {0,1}
            };
    }
    
    class LGapDLPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,0},
               {0,0},
               {1,1}
            };
    }

    class LGapDRPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {0,1},
               {0,0},
               {1,1}
            };
    }

    class LGapLUPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,0,1},
               {1,0,0},               
            };
    }

    class LGapLDPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,0,0},
               {1,0,1},               
            };
    }

    class LGapRUPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {1,0,1},
               {0,0,1},               
            };
    }

    class LGapRDPattern :PatternEval
    {
        protected override int[,] Pattern { get=>_Pattern;}

        static readonly int[,] _Pattern = new int[,] {
               {0,0,1},
               {1,0,1},               
            };
    }

}
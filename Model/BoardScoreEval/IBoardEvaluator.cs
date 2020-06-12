using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver.Model.BoardScoreEval
{
    interface IBoardEvaluator
    {
        int EvalBoard(Board board);
    }

    class EvaluatorCollection : List<IBoardEvaluator>, IBoardEvaluator
    {
        public int EvalBoard(Board board)
        {
            return this.Sum(x => x.EvalBoard(board));
        }
    }
}

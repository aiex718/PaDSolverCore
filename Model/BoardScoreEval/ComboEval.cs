using PaDSolver.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver.Model.BoardScoreEval
{
    class ComboEval: IBoardEvaluator
    {
        public int Score { get; set; }

        public int EvalBoard(Board board)
        {
            int Combo = 0;

            int TempCombo;
            do
            {
                var ClearedPoints = Check3ConnectedPoints(board);
                var ConnectedLabelsMap = ConnComponent.Search(board.Get2DBeads(), board.BeadTypesCount);
                var ClearedLabelId = ClearedPoints.Select(p => ConnectedLabelsMap[p.X, p.Y]);

                TempCombo = ClearedLabelId.Distinct().Count();

                foreach (var p in ClearedPoints)
                    board.RemoveBead(p.X, p.Y);

                board.Drop();
                Combo += TempCombo;
            } while (TempCombo > 0);

            return Combo * Score;
        }

        IEnumerable<Point> Check3ConnectedPoints(Board board)
        {
            List<Point> Result = new List<Point>();
            //Check X
            for (int y = 0; y < board.Height; y++)
            {
                for (int x = 0; x <= board.Width - 3; x++)
                {
                    int[] bead=new int[] { board.GetBead(x, y), board.GetBead(x + 1, y), board.GetBead(x + 2, y) };

                    if (bead[0]>=0 && bead[1]>=0 && bead[2]>=0 &&
                        bead[0]==bead[1]&& bead[1] == bead[2])
                    {
                        Result.Add(new Point(x, y));
                        Result.Add(new Point(x+1, y));
                        Result.Add(new Point(x+2, y));
                    }
                }
            }

            //Check Y
            for (int y = 0; y <= board.Height - 3; y++)
            {
                for (int x = 0; x < board.Width; x++)
                {
                    int[] bead = new int[] { board.GetBead(x, y), board.GetBead(x, y+1), board.GetBead(x, y + 2) };
                    if (bead[0] >= 0 && bead[1] >= 0 && bead[2] >= 0 &&
                        bead[0] == bead[1] && bead[1] == bead[2])
                    {
                        Result.Add(new Point(x, y));
                        Result.Add(new Point(x, y+1));
                        Result.Add(new Point(x, y+2));
                    }
                }
            }

            return Result.Distinct();
        }

    }
}

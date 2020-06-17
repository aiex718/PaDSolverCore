using PaDSolver.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver.Model.BoardScoreEval
{
    class ComboEvaluator: IBoardEvaluator
    {
        public int PerScore { get; set; }=1;

        public int EvalBoard(Board board)
        {
            int ScoreResult = 0;

            //board = board.Clone();

            bool Continue=true;
            do
            {
                var ClearedPoints = Check3ConnectedPoints(board);
                var ConnectionMap = ConnComponent.Search(board.Get2DBeads(), board.BeadTypesCount);                
                var ClearedLabelId = ClearedPoints.Select(p => ConnectionMap[p.X, p.Y]).Distinct();

                if (board.HasWeight)
                {
                    var ClearedLabel_BeadType_Dict = ClearedPoints.Select(
                        p => new { Label=ConnectionMap[p.X, p.Y],BeadType = board.GetBead(p.X,p.Y)}
                    ).GroupBy(x=>x.Label).ToDictionary(g=>g.Key,g=>g.First().BeadType);

                    foreach (var label in ClearedLabelId)
                    {
                        double weight = board.Weights[ClearedLabel_BeadType_Dict[label]];
                        ScoreResult+=(int)(weight*PerScore);
                    }
                }
                else
                     ScoreResult+=ClearedLabelId.Count()*PerScore;
                
                

                foreach (var p in ClearedPoints)
                    board.RemoveBead(p.X, p.Y);

                board.Drop();

                Continue = ClearedPoints.Any();
            } while (Continue);

            return ScoreResult;
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

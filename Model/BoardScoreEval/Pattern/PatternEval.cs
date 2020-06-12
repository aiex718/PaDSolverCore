using PaDSolver.Model.BoardScoreEval;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver.Model.Pattern
{
   abstract class PatternEval : IBoardEvaluator
   {
       protected abstract int[,] Pattern { get; }
       public int PerScore { get; set; }=1;

       public int EvalBoard(Board board)
       {
           int ScoreResult = 0;

           int PatternWidth = Pattern.GetLength(1);
           int PatternHeight = Pattern.GetLength(0);

           for (int w = 0; w <= board.Width - PatternWidth; w++)
           {
               for (int h = 0; h <= board.Height - PatternHeight; h++)
               {
                    int? FoundBead=null;
                    bool Continue = true;

                    for (int pw = 0; pw < PatternWidth && Continue; pw++)
                    {
                        for (int ph = 0; ph < PatternHeight && Continue; ph++)
                        {
                            if (Pattern[ph, pw] != 0)
                            {
                                var NewBead = board.GetBead(w + pw, h + ph);
                                    if (FoundBead==null)
                                        FoundBead = NewBead;
                                    else if(FoundBead != NewBead)
                                        Continue=false;
                            }
                        }
                    }

                    if (Continue)
                    {
                        if (board.HasWeight)
                        {
                            double weight = board.Weights[FoundBead.Value];
                            ScoreResult+=(int)(weight*PerScore);
                        }
                        else
                        {
                            ScoreResult+=PerScore;
                        }
                    }
               }
           }

           return ScoreResult;
       }
   }
}

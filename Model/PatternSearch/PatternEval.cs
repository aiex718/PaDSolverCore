//using PaDSolver.Model.BoardScoreEval;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PaDSolver.Model.PatternSearch
//{
//    class PatternEval : IBoardEvaluator
//    {
//        public int[,] Pattern { get; set; }
//        public int Score { get; set; }


//        public int EvalBoard(Board board)
//        {
//            int PatternWidth = Pattern.GetLength(1);
//            int PatternHeight = Pattern.GetLength(0);

//            int HitCount = 0;

//            for (int w = 0; w < board.Width - PatternWidth; w++)
//            {
//                for (int h = 0; h < board.Height - PatternHeight; h++)
//                {
//                    List<int> Cmp = new List<int>();

//                    for (int pw = 0; pw < PatternWidth; pw++)
//                    {
//                        for (int ph = 0; ph < PatternHeight; ph++)
//                        {
//                            if (Pattern[pw, ph] != 0)
//                            {
//                                Cmp.Add(board.GetBead(w + pw, h + ph));
//                            }
//                        }
//                    }

//                    if (Cmp.Any() && Cmp.Distinct().Count() == 1)
//                        ++HitCount;
//                }
//            }

//            return HitCount * Score;
//        }

//        static public PatternEval HoriConn6(int score)
//        {
//            return new PatternEval()
//            {
//                Score = score,
//                Pattern = new int[,] {
//                {1,1,1,1,1,1},
//                }
//            };
//        }

//        static public PatternEval HoriConn5(int score)
//        {
//            return new PatternEval()
//            {
//                Score = score,
//                Pattern = new int[,] {
//                {1,1,1,1,1},
//                }
//            };
//        }

//        static public PatternEval VertConn5(int score)
//        {
//            return new PatternEval()
//            {
//                Score = 1000,
//                Pattern = new int[,] {
//                {1},
//                {1},
//                {1},
//                {1},
//                {1},
//                }
//            };
//        }

//        static public PatternEval StarConn5(int score)
//        {
//            return new PatternEval()
//            {
//                Score = score,
//                Pattern = new int[,] {
//                {0,1,0},
//                {1,1,1},
//                {0,1,0}
//                }
//            };
//        }

//        static public PatternEval LLUConn5(int score)
//        {
//            return new PatternEval()
//            {
//                Score = score,
//                Pattern = new int[,] {
//                {1,1,1},
//                {1,0,0},
//                {1,0,0}
//                }
//            };
//        }

//        static public PatternEval LLDConn5(int score)
//        {
//            return new PatternEval()
//            {
//                Score = score,
//                Pattern = new int[,] {
//                {1,0,0},
//                {1,0,0},
//                {1,1,1}
//                }
//            };
//        }

//        static public PatternEval LRUConn5(int score)
//        {
//            return new PatternEval()
//            {
//                Score = score,
//                Pattern = new int[,] {
//                {1,1,1},
//                {0,0,1},
//                {0,0,1}
//                }
//            };
//        }

//        static public PatternEval LRDConn5(int score)
//        {
//            return new PatternEval()
//            {
//                Score = score,
//                Pattern = new int[,] {
//                {0,0,1},
//                {0,0,1},
//                {1,1,1}
//                }
//            };
//        }

//        static public PatternEval TUConn5(int score)
//        {
//            return new PatternEval()
//            {
//                Score = score,
//                Pattern = new int[,] {
//                {1,1,1},
//                {0,1,0},
//                {0,1,0}
//                }
//            };
//        }

//        static public PatternEval TDConn5(int score)
//        {
//            return new PatternEval()
//            {
//                Score = score,
//                Pattern = new int[,] {
//                {0,1,0},
//                {0,1,0},
//                {1,1,1}
//                }
//            };
//        }

//        static public PatternEval TLConn5(int score)
//        {
//            return new PatternEval()
//            {
//                Score = score,
//                Pattern = new int[,] {
//                {1,0,0},
//                {1,1,1},
//                {1,0,0}
//                }
//            };
//        }

//        static public PatternEval TRConn5(int score)
//        {
//            return new PatternEval()
//            {
//                Score = score,
//                Pattern = new int[,] {
//                {0,0,1},
//                {1,1,1},
//                {0,0,1}
//                }
//            };
//        }

//        static public PatternEval HoriConnect4Pattern(int score)
//        {
//            return new PatternEval()
//            {
//                Score = score,
//                Pattern = new int[,] {
//                { 1, 1, 1,1 }
//                }
//            };
//        }

//        static public PatternEval VertConnect4Pattern(int score)
//        {
//            return new PatternEval()
//            {
//                Score = score,
//                Pattern = new int[,] {
//                {1},
//                {1},
//                {1},
//                {1},
//                }
//            };
//        }

//        static public PatternEval HoriConnect3Pattern(int score)
//        {
//            return new PatternEval()
//            {
//                Score = score,
//                Pattern = new int[,] {
//                { 1, 1, 1 }
//                }
//            };
//        }

//        static public PatternEval VertConnect3Pattern(int score)
//        {
//            return new PatternEval()
//            {
//                Score = score,
//                Pattern = new int[,] {
//                {1},
//                {1},
//                {1},
//                }
//            };
//        }

//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver.Core
{
    class ConnComponent
    {
        public static int[,] Search(int[,] ary,int MaxElement)
        {
            //First Pass
            int[,] Result = new int[ary.GetLength(0), ary.GetLength(1)];

            int Label = 1;

            for (int val = 0; val < MaxElement; val++)
            {
                int[,] LabeledArray = new int[ary.GetLength(0), ary.GetLength(1)];
                Array.Clear(LabeledArray, 0, LabeledArray.Length);
                Dictionary<int, List<int>> ConnectedLabels = new Dictionary<int, List<int>>() {};
                               
                for (int y = 0; y < ary.GetLength(1); y++)
                {
                    for (int x = 0; x < ary.GetLength(0); x++)
                    {
                        if (ary[x, y] == val)
                        {
                            //check left
                            int LabelLeft = -1;
                            int LeftX = x - 1;
                            if (LeftX >= 0)
                                LabelLeft = LabeledArray[LeftX, y];

                            //check up
                            int LabelUp = -1;
                            int UpY = y - 1;
                            if (UpY >= 0)
                                LabelUp = LabeledArray[x, UpY];

                            int LabelSelect = -1;
                            if (LabelLeft > 0 && LabelUp > 0)
                            {
                                if (LabelLeft != LabelUp)
                                {
                                    LabelSelect = Math.Min(LabelLeft, LabelUp);

                                    //Save ConnectedLabels
                                    if (ConnectedLabels.TryGetValue(LabelLeft, out var LeftLabelList))
                                    {
                                        LeftLabelList.Add(LabelUp);
                                        LeftLabelList.Add(LabelLeft);
                                    }
                                    else
                                        ConnectedLabels.Add(LabelLeft, new List<int>() { LabelLeft, LabelUp });

                                    if (ConnectedLabels.TryGetValue(LabelUp, out var UpLabelList))
                                    {
                                        LeftLabelList.Add(LabelUp);
                                        LeftLabelList.Add(LabelLeft);
                                    }
                                    else
                                        ConnectedLabels.Add(LabelUp, new List<int>() { LabelLeft, LabelUp });
                                }
                                else
                                    LabelSelect = LabelLeft;
                            }
                            else if (LabelLeft > 0)
                                LabelSelect = LabelLeft;
                            else if (LabelUp > 0)
                                LabelSelect = LabelUp;
                            else
                            {
                                LabelSelect = Label++;
                                ConnectedLabels.Add(LabelSelect, new List<int>() { LabelSelect });
                            }

                            LabeledArray[x, y] = LabelSelect;

                        }

                    }
                }

                //var keys = ConnectedLabels.Select(x => x.Key).ToArray();
                //for (int i = 0; i < keys.Length; i++)
                //    ConnectedLabels[i] = ConnectedLabels[keys[i]].Distinct().ToList();


                //Second Pass
                for (int y = 0; y < ary.GetLength(1); y++)
                {
                    for (int x = 0; x < ary.GetLength(0); x++)
                    {
                        if (LabeledArray[x, y] > 0)
                        {
                            LabeledArray[x, y] = ConnectedLabels[LabeledArray[x, y]].Min();
                        }
                    }
                }

                //Update Label
                for (int y = 0; y < ary.GetLength(1); y++)
                {
                    for (int x = 0; x < ary.GetLength(0); x++)
                    {
                        if(LabeledArray[x,y]>0)
                            Result[x, y] = LabeledArray[x, y];                        
                    }
                }
            }


            return Result;
        }
    }
}

using Newtonsoft.Json;
using PaDSolver.Model.BoardScoreEval;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaDSolver.Core.Extensions;

namespace PaDSolver.Model
{
    public class Board
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<int> Beads { get; set; }
        public List<float> Weights { get; set; }
        public int SelectStartX { get; set; }
        public int SelectEndX { get; set; }
        public int SelectStartY { get; set; }
        public int SelectEndY { get; set; }
        public int StepLimit { get; set; }
        public int MoveDirection { get; set; }
        public int TargetScore { get; set; }

        public int Length => Width * Height;
        public int BeadTypesCount => Weights.Count;

        public bool HasWeight {
            get
            {
                if(_HasWeight.HasValue)
                    return _HasWeight.Value;
                else
                    _HasWeight=Weights.Distinct().Count()>1;
                return _HasWeight.Value;
            }
        }
        bool? _HasWeight=null;

        public int[,] Get2DBeads()
        {
            int[,] Beads2D = new int[Width, Height];
            for (int w = 0; w < Width; w++)
            {
                for (int h = 0; h < Height; h++)
                {
                    Beads2D[w, h] = Beads[w + Width * h];
                }
            }
            return Beads2D;
        }

        public void Random(int w,int h,int BeadTypes,Random rnd=null)
        {
            Random rand = rnd?? new Random(Guid.NewGuid().GetHashCode());
                        
            Width = w;
            Height = h;
            Weights = new List<float>();
            Beads = new List<int>();

            for (int i = 0; i < BeadTypes; i++)
                Weights.Add(1);

            IBoardEvaluator eval = new ComboEvaluator();

            do
            {
                Beads.Clear();
                for (int i = 0; i < Length; i++)
                    Beads.Add(rand.Next(BeadTypes));
            } while (eval.EvalBoard(Clone())>0);
                
        }

        public string Dump()
        {
            return Beads.Dump(Width);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public int GetBead(int x, int y)
        {
            return Beads[x + y * Width];
        }

        public void RemoveBead(int x, int y)
        {
            Beads[x + y * Width] = -1 ;
        }

        public void Drop()
        {
            for (int h = 0; h <Height; h++)
            {
                for (int w = 0; w <Width; w++)
                {
                    if (GetBead(w,h)<0)//self null, move up
                    {
                        var StartP = new Point(w, h);
                        while (StartP.Y>0)
                        {
                            StartP = MoveBeads(StartP, "U");                            
                        } 
                    }
                }
            }
        }

        public Board Clone()
        {
            Board b = this.MemberwiseClone() as Board;
            b.Beads = this.Beads.ToList();
            b.Weights = this.Weights.ToList();

            return b;
        }

        public void ApplyRoute(Route route)
        {
            var Start = new Point(route.StartX, route.StartY);
            foreach (var dir in route.Directions)
            {
                Start = MoveBeads(Start, dir);
            }
        }

        public Point MoveBeads(Point p,string dir)
        {
            int x = p.X, y = p.Y;
            int startIdx = x + y * Width;

            if (dir.Contains('L'))
                --x;
            else if (dir.Contains('R'))
                ++x;

            if (dir.Contains('U'))
                --y;
            else if (dir.Contains('D'))
                ++y;

            int endIdx = x + y * Width;

            if (x<0||y<0||x>=Width||y>=Height)
                return p;//MoveFail

            int temp = Beads[startIdx];
            Beads[startIdx] = Beads[endIdx];
            Beads[endIdx] = temp;

            return new Point(x, y);
        }


        public void SetMaxTargetScore()
        {
            var beadgroups = Beads.GroupBy(x=>x);
            TargetScore = beadgroups.Select(x=>x.Count()/3*1000).Sum();
        }
    }
}

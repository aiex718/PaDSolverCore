using Newtonsoft.Json;
using PaDSolver.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver.Model
{
    public class Route
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public List<string> Directions { get; set; }
        public double Score { get; set; }
        public List<int> Result { get; set; }
        public int TimeComsumedMs { get; set; }
        public int Iteration { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string Dump(int Width)
        {
            return Result.Dump(Width);
        }

    }

}

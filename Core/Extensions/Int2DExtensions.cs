using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver.Core.Extensions
{
    public static class Int2DExtensions
    {
        public static string Dump(this int[,] ary)
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < ary.GetLength(1); y++)
            {
                for (int x = 0; x < ary.GetLength(0); x++)
                {
                    sb.Append(ary[x,y].ToString().PadLeft(2));
                    sb.Append(',');
                }
                sb.Length--;
                sb.AppendLine();
            }

            sb.AppendLine();
            return sb.ToString();
        }

        public static string Dump(this IEnumerable<int> ary, int w)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ary.Count(); i++)
            {
                sb.Append(ary.ElementAt(i).ToString().PadLeft(2));
                sb.Append(',');
                if (i % w == w - 1)
                {
                    sb.Length--;
                    sb.AppendLine();
                }
            }
            sb.AppendLine();
            return sb.ToString();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDSolver.Model.Solver
{
    interface ISolver
    {
        Task<Route> SolveBoard(Board board);
        int ThreadCount{get;set;}
        bool EnableScoreDrop{get;set;}
        int ScoreDropPerSec{get;set;}
    }
}

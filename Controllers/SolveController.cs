using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaDSolver.Model;
using PaDSolver.Model.Solver;
using Newtonsoft;
using Newtonsoft.Json;

namespace PaDSolver.Controllers
{
    
    [ApiController]
    public class SolveController : ControllerBase
    {
        [HttpPost]
        [Route("api/solve/")]
        public async Task<ActionResult<string>> Post([FromQuery]string solvername,[FromBody] Board board)
        {
            //var board = JsonConvert.DeserializeObject<Board>(boardjson);
            SolverFactory factory = new SolverFactory(solvername);
            var solver = factory.GenSolver();
            solver.ThreadCount=6;
            solver.EnableScoreDrop = true;
            solver.ScoreDropPerSec = 200;
            Console.Write("Receive board:");
            Console.WriteLine(board.ToString());
            Console.WriteLine(board.Dump());

            var route = await solver.SolveBoard(board);
            Console.Write("Solved:");
            Console.WriteLine(route.ToString());

            return JsonConvert.SerializeObject(route);
        }

    }
}

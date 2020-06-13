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
    public class BoardController : ControllerBase
    {
        //GET test
        [HttpGet]
        [Route("api/board/")]
        public ActionResult<string> Get()
        {
            Board b = new Board();
            b.Random(6, 5, 6);
            b.SelectStartX = 0;
            b.SelectEndX = b.Width;
            b.SelectStartY = 0;
            b.SelectEndY = b.Height;
            b.StepLimit = 40;
            b.MoveDirection = 4;
            b.TargetScore = 6000;

            return JsonConvert.SerializeObject(b);
        }
    }
}

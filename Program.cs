﻿using PaDSolver.Core;
using PaDSolver.Core.Extensions;
using PaDSolver.Model;
using System.IO;
using PaDSolver.Model.BoardScoreEval;
using PaDSolver.Model.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ServiceStack;
using Funq;
using System.Net;

namespace PaDSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input port:");
            var port = Convert.ToInt32(Console.ReadLine());//5000            
            
            var host = new WebHostBuilder()
                .UseKestrel(options => {options.Listen(new IPAddress(0), port);})
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();

            // int Round=1000;
            // int ThreadSet=6;
            // bool EnScoreDrop=false;
            // int rndSeed=12345678;//12345678//Guid.NewGuid().GetHashCode();

            // var Result = ( new Benchmark( new SolverFactory(nameof(LinkSolver))){ 
            //     RoundCount=Round,
            //     ThreadCount=ThreadSet,
            //     Rand=new Random(rndSeed),                
            //     TargetScore=6000,
            //     BeadTypes=6,
            //     EnableScoreDrop=EnScoreDrop,
            //     ScoreDropPerSec=300
            // }.Start()).Result;
            // Result.Print();
        }

    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }


}

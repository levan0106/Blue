using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Startup;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace API.AccountMicroservice
{
    public class Program:BaseProgram<Startup>
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        //public static IWebHost BuildWebHost(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .ConfigureAppConfiguration(config =>
        //        {
        //            config.AddJsonFile("appsettings.json");
        //            config.AddEnvironmentVariables();
        //        })
        //        .UseStartup<Startup>()
        //        .Build();
    }
}

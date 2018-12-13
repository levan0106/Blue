using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using API.Core.Startup;

namespace API.Microservice.Store
{
    public class Program:BaseProgram<Startup>
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
        
    }
}

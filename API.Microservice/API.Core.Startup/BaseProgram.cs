
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace API.Core.Startup
{
    public class BaseProgram<T> where T: class
    {
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile("appsettings.json");
                    config.AddEnvironmentVariables();
                })
                .UseStartup<T>()
                .Build();
    }
}

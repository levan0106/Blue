using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Entities;
using API.Microservice.Authentication.Repositories;
using API.Microservice.Authentication.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace API.AuthService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOptions();
            services.Configure<Audience>(Configuration.GetSection("Audience"));

            //Dependency injection
            ResolveDependency(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //log4net
            var loggingOptions = this.Configuration.GetSection("Log4NetCore")
                                               .Get<Log4NetProviderOptions>();
            if (loggingOptions != null)
                loggerFactory.AddLog4Net(loggingOptions);
            else
                loggerFactory.AddLog4Net();

            app.UseMvc();
        }
        public void ResolveDependency(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            
        }
    }
}

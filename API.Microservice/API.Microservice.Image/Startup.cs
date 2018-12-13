using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Startup;
using API.Microservice.Image.Repositories;
using API.Microservice.Image.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace API.ImageMicroservice
{
    public class Startup:BaseStartup
    {
        public Startup(IConfiguration configuration):base(configuration)
        {
        }
        public override void ResolveDependency(IServiceCollection services)
        {
            services.AddTransient<IImageRepository, ImageRepository>();

            base.ResolveDependency(services);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Startup;
using API.Microservice.Store.Repositories;
using API.Microservice.Store.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace API.Microservice.Store
{
    public class Startup:BaseStartup
    {
        public Startup(IConfiguration configuration):base(configuration)
        {
        }
        public override void ResolveDependency(IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            base.ResolveDependency(services);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.EventBus.Consumer;
using API.Microservice.Notification.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace API.Microservice.Notification
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                        
            // Setup CORS
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin(); // For anyone access.
            //corsBuilder.WithOrigins("http://localhost:56573"); // for a specific url. Don't add a forward slash on the end!
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", corsBuilder.Build());
            });

            // SignalR
            services.AddSignalR();

            //Event Bus
            services.AddSingleton<RabbitMQListener>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            // USE CORS - might not be required.
            app.UseCors("AllowAll");

            app.UseMvc();

            // SignalR
            app.UseSignalR(routes =>
            {
                routes.MapHub<StocHub>("/stochub");
            });

            //Event bus
            app.UseRabbitListener();
        }
    }
    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            RegisterEventBus.UseRabbitListener(app, "notification-service");
            return app;
        }
    }
}

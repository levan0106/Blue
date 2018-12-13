using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System.Security.Claims;

namespace API.Core.Startup
{
    public class BaseStartup
    {
        private string _clientId;
        private string _userId;
        private static bool _enableLogging;
        public IConfiguration Configuration { get; }

        public BaseStartup(IConfiguration configuration, bool enableLogging=true)
        {
            Configuration = configuration;
            _enableLogging = enableLogging;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var audienceConfig = Configuration.GetSection("Audience");

            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Iss"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Aud"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = "TestKey";
            })
            .AddJwtBearer("TestKey", x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = tokenValidationParameters;
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                       // _clientId = context.Principal.FindFirst(ClaimTypes.Webpage).Value;
                       // _userId = context.Principal.Identity.Name;
                        return Task.CompletedTask;
                    }
                };

            });
            services.AddMvc();

            ResolveDependency(services);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAuthentication();

            if(_enableLogging)
            {
                //log4net
                var loggingOptions = this.Configuration.GetSection("Log4NetCore")
                                                   .Get<Log4NetProviderOptions>();
                if (loggingOptions != null)
                    loggerFactory.AddLog4Net(loggingOptions);
                else
                    loggerFactory.AddLog4Net();
            }
            app.Use(async (context, next) =>
            {
                // perform some verification
               // context.Items["ClientId"] = _clientId;
               // context.Items["UserId"] = _userId;
                await next.Invoke();
            });

            app.UseMvc();
        }

        public virtual void ResolveDependency(IServiceCollection services)
        {

        }
    }
}

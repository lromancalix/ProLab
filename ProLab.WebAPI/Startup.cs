using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProLab.DataAccess;
using ProLab.UnitOfWork;
using ProLab.WebAPI.Authentication;

namespace ProLab.WebAPI
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

            services.AddSingleton<IUnitOfWork>(option => new ProLabUnitOfWork(this.getDBConnetionString()));

            // Auto Mapper
            services.AddAutoMapper(typeof(Startup));

            services = this.SetTokenToService(services);

            //<swagger>
            services = this.SwaggerConfig(services);
            //</swagger>

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "ProLapAPI");
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region MIS CONFIGURACIONES

        /// <summary>
        /// Se obtiene el ambiente utilizado, definido en el json config.
        /// </summary>
        /// <returns></returns>
        private string getDBConnetionString()
        {
            string connectionString = "";

            bool valor = Convert.ToBoolean(Configuration.GetSection("Enviroment").GetSection("local").GetSection("isActive").Value);

            if (Convert.ToBoolean(Configuration.GetSection("Enviroment").GetSection("local").GetSection("isActive").Value))
            {
                connectionString = Configuration.GetSection("Enviroment").GetSection("local").GetSection("connectiosString").Value;
            }
            else if (Convert.ToBoolean(Configuration.GetSection("Enviroment").GetSection("dev").GetSection("isActive").Value))
            {
                connectionString = Configuration.GetSection("Enviroment").GetSection("dev").GetSection("connectiosString").Value;
            }
            else if (Convert.ToBoolean(Configuration.GetSection("Enviroment").GetSection("qa").GetSection("isActive").Value))
            {
                connectionString = Configuration.GetSection("Enviroment").GetSection("qa").GetSection("connectiosString").Value;
            }
            else if (Convert.ToBoolean(Configuration.GetSection("Enviroment").GetSection("prd").GetSection("isActive").Value))
            {
                connectionString = Configuration.GetSection("Enviroment").GetSection("prd").GetSection("connectiosString").Value;
            }


            return connectionString;
        }

        /// <summary>
        /// Configuración y validación del token.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection SetTokenToService(IServiceCollection services)
        {
            //<Authentication JWT>
            var tokenProvider = new JwtProvider("iusser", "audience", "tokenProvider");

            services.AddSingleton<ITokenProvider>(tokenProvider);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = tokenProvider.GetTokenValidationParameters();
                });

            services.AddAuthorization(auth =>
            {
                auth.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
            });
            ///<Authentication JWT>
            ///
            return services;
        }


        /// <summary>
        /// Configuración para el uso de swagger.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection SwaggerConfig(IServiceCollection services) => services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo { Title = "ProLapAPI", Version = "v1" });
        });

        #endregion

    }
}

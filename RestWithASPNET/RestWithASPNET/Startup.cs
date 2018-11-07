using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestWithASPNET.Model.Context;
using RestWithASPNET.Services.Business;
using RestWithASPNET.Services.Repository;
using System;
using System.Collections.Generic;

namespace RestWithASPNET
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly ILogger logger;
        public IHostingEnvironment environment { get; } 
        
        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            Configuration = configuration;
            this.environment = environment;
            this.logger = logger;
        }       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["MySqlConnection:MySqlConnection"];
            services.AddDbContext<MySqlContext>(options => options.UseMySql(connectionString));
            if(environment.IsDevelopment())
            {
                try
                {
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                    var evolve = new Evolve.Evolve("evolve.json", evolveConnection, msg => logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "db/migrations" },
                        IsEraseDisabled = true,
                    };
                    evolve.Migrate();
                }
                catch(Exception ex)
                {
                    logger.LogCritical("Database Migration failed", ex);
                    throw;
                }
            }
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);           
            //Dependence Injection
            services.AddScoped<IPersonBusiness, PersonBusinessImpl>();
            services.AddScoped<IPersonRepository, PersonRepositoryImpl>();
            //Add Versioning 
            services.AddApiVersioning();
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
            app.UseMvc();
        }
    }
}

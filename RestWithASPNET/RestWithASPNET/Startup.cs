using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestWithASPNET.Model.Context;
using RestWithASPNET.Services.Business;
using RestWithASPNET.Services.Repository;

namespace RestWithASPNET
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
            var connection = Configuration["MySqlConnection:MySqlConnection"];
            services.AddDbContext<MySqlContext>(options => options.UseMySql(connection));
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

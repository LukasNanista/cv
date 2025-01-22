using HotelAppLibrary.Data;
using HotelAppLibrary.Databases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.Webb
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
            services.AddRazorPages();
            //dependency injection, top-down version, that we added so we can use our db
            //dependency inversion principle in practice
            //mapping interface to a type here (e.g. IDatabaseData to SqlData)
            //using transient will create instance every time we ask for it - we want everyone to have their own connection to db
            //singleton would create one instance for the entire application
            //lastly (chronologically), adding toggle for different dbs
            string dbChoice = Configuration.GetValue<string>("DatabaseChoice").ToLower();
            if (dbChoice == "sql")
            {
                services.AddTransient<IDatabaseData, SqlData>();
            }
            else if (dbChoice== "sqlite")
            {
                services.AddTransient<IDatabaseData, SqliteData>();
            }
            else
            {
                //fallback / default value
                services.AddTransient<IDatabaseData, SqlData>();
            }

            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            services.AddTransient<ISqliteDataAccess, SqliteDataAccess>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}

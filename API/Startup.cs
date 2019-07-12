using Core;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace API
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
            services.AddDbContext<StoreLocatorContext>(ob => ob.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"),
                b => b.UseNetTopologySuite()));

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new Info());
                o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "API.xml"));
            });

            services.AddScoped<IStoresRepository, StoresRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // TODO Rework to middleware
                app.UseExceptionHandler(ab => ab.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                    string result = JsonConvert.SerializeObject(new
                    {
                        error = feature.Error
                    });

                    await context.Response.WriteAsync(result);
                }));
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(o =>
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreLocator API"));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

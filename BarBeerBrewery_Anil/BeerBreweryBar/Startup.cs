using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.EntityFrameworkCore;
using BeerBreweryBar.Data;
using BeerBreweryBar.Interfaces;
using BeerBreweryBar.Models;
using BeerBreweryBar.Services;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Builder;
using AutoMapper;
using BeerBreweryBar.Models.POCO;

namespace BeerBreweryBar
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
            services.AddControllers();

            services.AddDbContext<BeerBreweryBarContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BeerBreweryBarContext")));
            //services.AddSwaggerGen();
            services.AddScoped<IBarService, BarService>();
            services.AddScoped<IBeerService, BeerService>();
            services.AddScoped<IBreweryService, BreweryService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddOData();
            services.AddODataQueryFilter();
            services.AddMvc(options
                                => options.EnableEndpointRouting = false);
            services.AddAutoMapper(typeof(Startup));
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Beer, BeerData>());
            services.AddSingleton(config.CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Beer Brewery Bar");
            //    c.RoutePrefix = String.Empty;
            //});
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Filter().Expand().OrderBy().Count().MaxTop(1000);
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });
        }
        private IEdmModel GetEdmModel()
        {
            var edmBuilder = new ODataConventionModelBuilder();
            edmBuilder.EntitySet<Beer>("BeersOdata");
            edmBuilder.EntitySet<Brewery>("BreweriesOdata");
            edmBuilder.EntitySet<Bar>("BarsOdata");
            return edmBuilder.GetEdmModel();
        }
    }
}

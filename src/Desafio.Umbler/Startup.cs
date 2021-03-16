using System;
using AutoMapper;
using Desafio.Umbler.Application;
using Desafio.Umbler.Application.Mappers;
using Desafio.Umbler.Infrastructure;
using Desafio.Umbler.Infrastructure.Interfaces;
using Desafio.Umbler.Models;
using Desafio.Umbler.Services;
using DnsClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio.Umbler
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
            // Add framework services.
            services.AddDbContext<DatabaseContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 21))));

            services.AddMvc(options => options.EnableEndpointRouting = false);

            // AutoMapper
            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new DomainProfile()); });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<ISearchDomainService, SearchDomainService>();
            services.AddScoped<IUmblerWhoisClient, UmblerWhoisClient>();
            services.AddScoped<ILookupClient, LookupClient>();
            services.AddScoped<ISearchDomainApplication, SearchDomainApplication>();

            // MvcOptions.EnableEndpointRouting = false
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

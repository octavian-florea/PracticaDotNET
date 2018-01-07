using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Formatters;
using NLog.Extensions.Logging;
using Practica.Core;
using Practica.Data;
using Practica.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Practica.WebAPI
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        } 

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
  
            services.AddScoped<IInternshipService, InternshipService>();
            services.AddScoped<IIntershipRepository, IntershipRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IActivityTypeRepository, ActivityTypeRepository>();
            services.AddScoped<IAplicationRepository, AplicationRepository>();
            services.AddTransient<DbInitializer>();
            services.AddSingleton<IConfiguration>(Configuration);

            var connectionString = Startup.Configuration["ConnectionString:PracticaConnection"];
            services.AddDbContext<PracticaContext>(o => o.UseSqlServer(connectionString));

            services.AddIdentity<PracticaUser, IdentityRole>()
                .AddEntityFrameworkStores<PracticaContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    if (context.Request.Path.StartsWithSegments("/api") && context.Response.StatusCode == 200)
                    {
                        context.Response.StatusCode = 401;
                    }
      
                    return Task.CompletedTask;
                };

                options.Events.OnRedirectToAccessDenied = context =>
                {
                    if (context.Request.Path.StartsWithSegments("/api") && context.Response.StatusCode == 200)
                    {
                        context.Response.StatusCode = 403;
                    }

                    return Task.CompletedTask;
                };
            });

           

            services.AddCors();

            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()
                    ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DbInitializer seeder)
        {
            loggerFactory.AddDebug();

            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod()
            );

            app.UseAuthentication();

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Activity, ActivityDto>();
                cfg.CreateMap<ActivityDto, Activity>();
                cfg.CreateMap<ActivityUpdateDto, Activity>();
                cfg.CreateMap<ActivityCreateDto, Activity>();
                cfg.CreateMap<AplicationCreateDto, Aplication>();
                cfg.CreateMap<Aplication, AplicationDto>(); 
            });

            app.UseMvc();

            seeder.Seed().Wait();
        }
    }
}

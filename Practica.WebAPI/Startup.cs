﻿using System;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Serialization;

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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            services.AddScoped<IInternshipService, InternshipService>();
            services.AddScoped<IIntershipRepository, IntershipRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IActivityTypeRepository, ActivityTypeRepository>();
            services.AddScoped<IAplicationRepository, AplicationRepository>();
            services.AddScoped<IUniversityRepository, UniversityRepository>();
            services.AddScoped<IFacultyRepository, FacultyRepository>();
            services.AddScoped<ICompanyProfileRepository, CompanyProfileRepository>();
            services.AddScoped<IStudentProfileRepository, StudentProfileRepository>();
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();
            
            services.AddTransient<DbInitializer>();
            services.AddSingleton<IConfiguration>(Configuration);

            // ===== Add our DbContext ========
            var connectionString = Startup.Configuration["ConnectionString:PracticaConnection"];
            services.AddDbContext<PracticaContext>(o => o.UseSqlServer(connectionString));

            // ===== Add Identity ========
            services.AddIdentity<PracticaUser, IdentityRole>()
               .AddEntityFrameworkStores<PracticaContext>()
               .AddDefaultTokenProviders();

            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidAudience = Configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"]))
                };
            });

            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()
                    ))
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DbInitializer seeder)
        {
            app.UseCors("AllowAll");

            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ActivityDto, Activity>().ReverseMap();
                cfg.CreateMap<ActivityUpdateDto, Activity>();
                cfg.CreateMap<ActivityCreateDto, Activity>();
                cfg.CreateMap<AplicationCreateDto, Aplication>();
                cfg.CreateMap<AplicationDto, Aplication>();
                cfg.CreateMap<Aplication, AplicationDto>()
                    .ForMember(
                        destination => destination.Name,
                        opts => opts.MapFrom(source => source.PracticaUser.StudentProfile.Name)
                    ).ForMember(
                        destination => destination.Description,
                        opts => opts.MapFrom(source => source.PracticaUser.StudentProfile.Description)
                    ).ForMember(
                        destination => destination.City,
                        opts => opts.MapFrom(source => source.PracticaUser.StudentProfile.City)
                    ).ForMember(
                        destination => destination.Telephone,
                        opts => opts.MapFrom(source => source.PracticaUser.StudentProfile.Telephone)
                    ).ForMember(
                        destination => destination.Email,
                        opts => opts.MapFrom(source => source.PracticaUser.Email)
                    ).ForMember(
                        destination => destination.Faculty,
                        opts => opts.MapFrom(source => source.Faculty.Name)
                    ).ForMember(
                        destination => destination.ActivityTitle,
                        opts => opts.MapFrom(source => source.Activity.Title)
                    ).ForMember(
                        destination => destination.ActivityType,
                        opts => opts.MapFrom(source => source.Activity.Type)
                    );
                cfg.CreateMap<AplicationUpdateDto, Aplication>();
                cfg.CreateMap<CompanyProfileDto, CompanyProfile>().ReverseMap();
                cfg.CreateMap<CompanyProfileViewDto, CompanyProfile>().ReverseMap();
                cfg.CreateMap<StudentProfileDto, StudentProfile>();
                cfg.CreateMap<StudentProfile, StudentProfileDto>().ForMember(
                    destination => destination.FacultyName,
                    opts => opts.MapFrom(source => source.Faculty.Name));
            });

            app.UseMvc();

            seeder.Seed().Wait();

        }
    }
}

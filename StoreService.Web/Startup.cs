using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using StoreService.Web.Models;
using StoreService.Web.Services;

namespace StoreService.Web
{
    public class Startup
    {
        ILogger<Startup> logger;
        private IHostingEnvironment env;

        public Startup(IConfiguration configuration, ILogger<Startup> logger,
            IHostingEnvironment env)
        {
            this.Configuration = configuration;
            this.logger = logger;
            this.env = env;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var conBuilder = new SqlConnectionStringBuilder(
                Configuration.GetConnectionString(nameof(StoreContext)));
            conBuilder.Password = Configuration["StorePassword"];

            if (env.IsDevelopment())
            {
                services.AddMvc(opts =>
                {
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            }
            else
            {
                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            }

            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IAuthorService, AuthorService>();

            services.AddDbContext<StoreContext>(builder =>
            {
                builder.UseSqlServer(
                    conBuilder.ConnectionString
                );

            });
            services.AddLogging();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Store API", Version= "v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            });

            var authDomain = $"https://{Configuration["AuthDomain"]}/";
            var authAudience = Configuration["AuthAudience"];

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options=>{
                options.Authority = authDomain;
                options.Audience = authAudience;
            });

            services.AddAuthorization(options => {
                options.AddPolicy("query", policy => policy.Requirements.Add(
                        new HasScopeRequirement("query", authDomain)
                    ));
                options.AddPolicy("add:author", policy => policy.Requirements.Add(
                        new HasScopeRequirement("add:author", authDomain)
                    ));
                options.AddPolicy("update:author", policy => policy.Requirements.Add(
                        new HasScopeRequirement("update:author", authDomain)
                    ));
                options.AddPolicy("delete:author", policy => policy.Requirements.Add(
                        new HasScopeRequirement("delete:author", authDomain)
                    ));
            });

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(options => {
                    options.AllowAnyOrigin();
                    options.AllowAnyHeader();
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c=> {
                c.SwaggerEndpoint("v1/swagger.json", "Store API V1");
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

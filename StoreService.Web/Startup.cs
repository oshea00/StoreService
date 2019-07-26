using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            //logger.LogInformation($"Secret: {Configuration["Kestrel:Certificates:Development:Password"]}");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var conBuilder = new SqlConnectionStringBuilder(
                Configuration.GetConnectionString(nameof(StoreContext)));
            conBuilder.Password = Configuration["StorePassword"];

            if (env.IsDevelopment())
            {
                services.AddMvc(opts =>
                {
                    opts.Filters.Add(new AllowAnonymousFilter());
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
                //c.IncludeXmlComments(xmlPath);
            });

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options=>{
                options.Authority = "https://dev--8x78t70.auth0.com";
                options.Audience = "https://StoreFuturTrends.com";
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

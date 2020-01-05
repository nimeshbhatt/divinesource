using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Domain.Context;
using Domain;
using Domain.Models;
using Service.Interface;
using Service.Services;
using AutoMapper;
using Domain.Handler;
using Core.Filters;

namespace Divine
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
            // Db Configuration
            services.AddDbContext<DivineContext>(
           options =>
               options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));


            // Mapper configuration
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Users, UsersAc>().ReverseMap();
                cfg.CreateMap<ServiceEngineer, ServiceEngineerAc>().ReverseMap();
                cfg.CreateMap<ServiceQuotation, ServiceQuotationAc>().ReverseMap();
                cfg.CreateMap<UploadDocumentImage, UploadDocumentImageAc>().ReverseMap();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            //Service Registration
            //services.AddTransient<IUserService, UserService>();
            //services.AddTransient<IServiceEngineer, ServiceEngineerService>();
            //services.AddTransient<IImageHandler, ImageHandler>();
            //services.AddTransient<IImageWriter, ImageWriter>();
            //services.AddTransient<IApiService, ApiService>();
            //services.AddTransient<IServiceQuotationService, ServiceQuotationService>();
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IServiceEngineer, ServiceEngineerService>();
            services.AddScoped<IImageHandler, ImageHandler>();
            services.AddScoped<IImageWriter, ImageWriter>();
            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<IServiceQuotationService, ServiceQuotationService>();

            //services.AddMvcCore().AddDataAnnotations();
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            })
            .AddJsonOptions(options => { options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
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

            //Use this to set path of files outside the wwwroot folder
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
            //    RequestPath = "/StaticFiles"
            //});

            app.UseStaticFiles(); //letting the application know that we need access to wwwroot folder.
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<DivineContext>();
                db.Database.Migrate();
            }
        }
    }
}

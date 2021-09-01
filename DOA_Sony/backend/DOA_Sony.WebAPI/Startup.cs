using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using DOA_Sony.BusinessLayer;
using DOA_Sony.BusinessLayer.Interfaces;
using DOA_Sony.DataLayer;
using System;
using System.Reflection;
using System.IO;
using System.Text.Json.Serialization;

namespace DOA_Sony.WebAPI
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
            services.AddControllers().AddJsonOptions(options =>
                             options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddDbContext<SonyServiceContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:SonyServiceDB"]));
            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddScoped<IControlService, ControlService>();
            services.AddScoped<ISeedService, SeedService>();
            services.AddScoped<IAPNSService, APNSService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DOA_Sony API",
                    Description = "Sony Warranty Service API",
                    TermsOfService = new Uri("https://www.oplog.com.tr"),
                    Contact = new OpenApiContact
                    {
                        Name = "OPLOG",
                        Email = "info@oplog.com.tr",
                        Url = new Uri("https://www.oplog.com.tr"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "OPLOG",
                        Url = new Uri("https://www.oplog.com.tr"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DOA_Sony API V1");
                c.RoutePrefix = "api";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

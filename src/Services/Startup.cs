using System;
using System.IO;
using AutoMapper;
using AutoMapper.Mappers;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Server.Kestrel.Core;

using ParcelLogistics.SKS.Package.Services.Filters;
using ParcelLogistics.SKS.Package.Services.Mapper;
using ParcelLogistics.SKS.Package.BusinessLogic.Interfaces;
using ParcelLogistics.SKS.Package.BusinessLogic;
using ParcelLogistics.SKS.Package.DataAccess.Sql;
using ParcelLogistics.SKS.Package.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ParcelLogistics.SKS.Package.Services.Helpers;
using ParcelLogistics.SKS.Package.ServiceAgents.Interfaces;
using ParcelLogistics.SKS.Package.ServiceAgents;

namespace ParcelLogistics.SKS.Package.Services
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnv;

        /// <summary>
        /// Startup class
        /// </summary>
        /// <param name="env"></param>
        /// <param name="configuration">Configuration of the application</param>
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _hostingEnv = env;
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration of the application.
        /// </summary>
        /// <value></value>
        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.AllowCredentials();
                    });
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            // Add framework services.
            services
                .AddAutoMapper(typeof(MappingProfile))
                .AddMvc()
                .AddJsonOptions(opts =>
                {
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter
                    {
                        CamelCaseText = true
                    });
                    opts.SerializerSettings.Converters.Add(new HopJsonConverter());
                })
                .AddXmlDataContractSerializerFormatters();

            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("1.3.0", new OpenApiInfo
                    {
                        Version = "1.3.0",
                        Title = "Parcel Logistics Service",
                        Description = "Parcel Logistics Service (ASP.NET Core 2.0)",
                        Contact = new OpenApiContact()
                        {
                            Name = "SKS",
                            Url = new Uri("http://www.technikum-wien.at/"),
                            Email = ""
                        }
                    });

                    //TODO: fix this
                    // c.CustomSchemaIds(type => type.FriendlyId(false));
                    //c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_hostingEnv.ApplicationName}.xml");

                    // Include DataAnnotation attributes on Controller Action parameters as Swagger validation rules (e.g required, pattern, ..)
                    // Use [ValidateModelState] on Actions to actually validate it in C# as well!
                    //c.OperationFilter<GeneratePathParamsValidationFilter>();
                });
            
            services.AddScoped<IExportImportLogic, ExportImportLogic>();
            services.AddScoped<ITrackingLogic, TrackingLogic>();
            services.AddScoped<ITrackingPointRepository, SqlTrackingPointRepository>();
            services.AddScoped<IWarehouseRepository, SqlWarehouseRepository>();
            services.AddScoped<IGeoEncodingAgent, BingGeoEncodingAgent>();

            // Add Implementation for Data Access Layer
            // MS SQL
            services.AddScoped<IParcelRepository, SqlParcelRepository>();
            services.AddDbContext<SqlContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DB"),p => p.UseNetTopologySuite());
            });

        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //TODO: Either use the SwaggerGen generated Swagger contract (generated from C# classes)
                c.SwaggerEndpoint("/swagger/1.3.0/swagger.json", "Parcel Logistics Service");

                //TODO: Or alternatively use the original Swagger contract that's included in the static files
                // c.SwaggerEndpoint("/swagger-original.json", "Parcel Logistics Service Original");
            });

           

            // app.UseHttpsRedirection();
            // app.UseAuthorization();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //TODO: Enable production exception handling (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling)
                // app.UseExceptionHandler("/Home/Error");
                app.UseExceptionHandler("/Error");
            }
        }
    }
}

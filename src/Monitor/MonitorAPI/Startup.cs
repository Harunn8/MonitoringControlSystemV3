using AutoMapper;
using McsCore.AppDbContext;
using McsCore.AppDbContext.Mongo;
using McsCore.Mongo;
using McsCore.Repositories;
using McsCore.Repositories.Base;
using McsMqtt.Connection;
using McsMqtt.Connection.Base;
using McsMqtt.Producer;
using McsMqtt.Settings;
using McsUserLogs.Services;
using McsUserLogs.Services.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MonitorApplication.Data.Interfaces;
using MonitorApplication.Mapper;
using MonitorApplication.Repository;
using MonitorApplication.Repository.Base;
using MonitorApplication.Services;
using MonitorApplication.Services.Base;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenInformation.Base;

namespace MonitorAPI
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

            #region Mqtt

            Console.WriteLine("Mqtt connection preparing...");

            services.Configure<MqttSettings>(Configuration.GetSection("Mqtt"));

            services.AddSingleton<IMqttClientOptions>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MqttSettings>>().Value;

                return new MqttClientOptionsBuilder()
                    .WithClientId("telemetry")
                    .WithTcpServer(settings.IpAddress, settings.Port)
                    .WithCleanSession()
                    .Build();
            });

            services.AddSingleton<IMqttClient>(sp =>
            {
                var factory = new MqttFactory();
                return factory.CreateMqttClient();
            });

            services.AddSingleton<IMqttConnection, MqttConnection>();
            services.AddSingleton<MqttProducer>();

            Console.WriteLine("Mqtt connection was establish");

            #endregion

            #region Postgres

            services.AddDbContext<McsAppDbContext>(options => options.UseNpgsql(
            Configuration.GetConnectionString("Postgres")));

            #endregion


            #region Mongo
            Log.Information("MongoDB connection preparing is started");

            var mongoDbSettings = Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();

            services.AddSingleton<IMongoClient>(sp =>
            {
                var connectionString = Configuration["ConnectionStrings:MongoDb"];
                return new MongoClient(connectionString);
            });

            services.AddSingleton(sp =>
            {
                var mongoClient = sp.GetRequiredService<IMongoClient>();
                return mongoClient.GetDatabase(mongoDbSettings.DatabaseName);
            });
            services.AddSingleton<MongoDbContext>();

            services.AddScoped<IUserLogService, UserLogService>();

            Log.Information("MongoDB connection was established");

            #endregion

            #region Services
            services.AddAutoMapper(typeof(MonitorMapper));
            services.AddScoped<IParameterLogRepository, ParameterLogRepository>();
            services.AddScoped<IParameterLogService, ParameterLogService>();
            services.AddScoped<IUserLogService, UserLogService>();

            #endregion

            services.AddControllers();
            services.AddSwaggerGen();




            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MonitorAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Start with Bearer{token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MonitorAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

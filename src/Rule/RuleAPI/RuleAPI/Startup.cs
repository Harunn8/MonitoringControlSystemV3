using McsCore.AppDbContext;
using McsCore.AppDbContext.Mongo;
using McsCore.Mongo;
using McsMqtt.Connection;
using McsMqtt.Connection.Base;
using McsMqtt.Producer;
using McsMqtt.Settings;
using McsUserLogs.Services;
using McsUserLogs.Services.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using RuleApplication.Mapper;
using RuleApplication.Services;
using RuleApplication.Services.Base;
using RuleApplication.Validations;
using Serilog;

namespace RuleAPI
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

            #region MQTT

            Log.Information("Mqtt connection preparing...");

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

            // MQTT client => Singleton
            services.AddSingleton<IMqttClient>(sp =>
            {
                var factory = new MqttFactory();
                return factory.CreateMqttClient();
            });

            // Producer sarmalayýcý => Singleton
            services.AddSingleton<IMqttConnection, MqttConnection>();
            services.AddScoped<MqttProducer>();

            Log.Information("Mqtt connection was establish");
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
            services.AddAutoMapper(typeof(RuleMappingProfile));
            services.AddScoped<IAlarmService, AlarmService>();
            services.AddScoped<IPolicyScriptService, PolicyScriptService>();
            services.AddScoped<IUserLogService, UserLogService>();

            services.AddTransient<PolicyScriptValidator>();
            services.AddTransient<AlarmValidator>();

            #endregion


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RuleAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RuleAPI v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

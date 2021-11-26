using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TeamsIntegration.Common.Configuration;
using TeamsIntegration.Common.Helper;
using TeamsIntegration.Common.Utilities;
using TeamsIntegration.Data;
using TeamsIntegration.SyncTool.Business;

namespace TeamsIntegration.SyncTool
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

        public static void Main(string[] args)
        {
            #region SeriLog
            //https://prasad-patil.medium.com/serilog-for-asp-net-core-3-1-c217b5aa8152
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(Configuration)
            .CreateLogger();
            #endregion
            try
            {
                Log.Information("Starting up the service");
                // var services = ConfigureServices();
                CreateHostBuilder(args).Build().Run();

                return;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "There was a problem starting the serivce");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    List<Organizations> oganizations = Configuration
              .GetSection(Constants.OrganizationsKey)
              .Get<List<Organizations>>();

                    #region DI
                    services.AddSingleton(oganizations);
                    services.AddScoped<IMappingBusiness, MappingBusiness>();
                    services.AddScoped<ITeamBusinessLayer, TeamBusinessLayer>();
                    services.AddScoped<ILMSBusinessLayer, LMSBusinessLayer>();
                    #endregion
                    services.AddDbContext<TeamDBContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                                        services.AddHostedService<Worker>();

                   

                  
                    
                });
        }
        

    }
}

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeamsIntegration.Common.Configuration;
using TeamsIntegration.Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using TeamsIntegration.Common.Helper;
using System.Globalization;
using Microsoft.Graph.ExternalConnectors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamsIntegration.Data;
using Microsoft.EntityFrameworkCore;
using TeamsIntegration.SyncTool.Business;

namespace TeamsIntegration.SyncTool
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly List<Organizations> _orgs;
        private readonly IMappingBusiness _mappingBusiness;

        public Worker(ILogger<Worker> logger, List<Organizations> orgs, IMappingBusiness mappingBusiness)
        {
            _logger = logger;
            _orgs = orgs;
            _mappingBusiness = mappingBusiness;




        }
     
        public override Task StartAsync(CancellationToken cancellationToken)
        {

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {

            _logger.LogInformation("The service has been stopped...");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
         
                await StartSync();
                int timerinMiliSeconds = Convert.ToInt32( AuthenticationConfig.Get("TimerinMiliSeconds"));
                await Task.Delay(timerinMiliSeconds, stoppingToken);
            }
        }

        private async Task StartSync()
        {
           
            Console.WriteLine(string.Format("Tool Started syncing at: {0}", DateTimeOffset.Now));
            
            foreach (Organizations organization in _orgs)
            {
               SyncCreatedTeams(organization);
            }
            Console.WriteLine(string.Format("Tool Finished syncing at: {0}", DateTimeOffset.Now));
            
        }

        private async void SyncCreatedTeams(Organizations organization)
        {
            Console.WriteLine(string.Format("---Tool Started SyncCreatedTeams a login token {0}", organization.loginToken));
          await  _mappingBusiness.CheckMappingLMSandEducational();
            //var ProtectedApiCallHelper = await RunAsync();
           // await ProtectedApiCallHelper.CreateTeam();
            // 1- Call LMS Api
           // var LmsCourses =await _businessLayer.GetLMSCourses();

            Console.WriteLine();
        }

        #region Run Fun 
        private async Task<GraphCallerHelper> RunAsync()
        {
            AuthenticationConfig config = AuthenticationConfig.ReadFromJsonFile("appsettings.json");
            string Authority = String.Format(CultureInfo.InvariantCulture, config.Instance, config.Organizations.FirstOrDefault().Tenants.FirstOrDefault().TenantID);

            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(config.Organizations.FirstOrDefault().Tenants.FirstOrDefault().ClientId)
                    .WithClientSecret(config.Organizations.FirstOrDefault().Tenants.FirstOrDefault().ClientSecret)
                    .WithAuthority(new Uri(Authority))
                    .Build();


            string[] scopes = new string[] { $"{config.ApiUrl}.default" };

            AuthenticationResult result = null;
            try
            {
                result = await app.AcquireTokenForClient(scopes)
                    .ExecuteAsync();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Token acquired");
                Console.ResetColor();
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
            {
                // Invalid scope. The scope has to be of the form "https://resourceurl/.default"
                // Mitigation: change the scope to be as expected
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Scope provided is not supported");
                Console.ResetColor();
            }
            if (result != null)
            {
                var authProvider = new DelegateAuthenticationProvider(async (request) =>
                {
                    request.Headers.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.AccessToken);
                });
                var graphClient = new GraphServiceClient(authProvider);
                var graphCaller = new GraphCallerHelper(graphClient);
                return graphCaller;
            }
            return null;
        }

        #endregion
    }
}

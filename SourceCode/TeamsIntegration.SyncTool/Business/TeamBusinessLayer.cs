using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TeamsIntegration.Common.Configuration;
using TeamsIntegration.Common.Helper;
using TeamsIntegration.SyncTool.ViewModels;
using System.Globalization;


namespace TeamsIntegration.SyncTool.Business
{
    public class TeamBusinessLayer : ITeamBusinessLayer
    {
       
        public async Task<List<EducationClassViewModel>> GetEducationalCourses()
        {
            List<EducationClassViewModel> courseViewModels = new List<EducationClassViewModel>();
           var _graphApiCallHelper =await RunAsync();
            var res = await _graphApiCallHelper.GetEducationalClasses();
            if (res.Count != 0)
            {
                foreach (var educationclass in res)
                {
                    EducationClassViewModel course = new EducationClassViewModel();
                    course.Id = educationclass.Id;
                    course.ExternalId = educationclass.ExternalId;
                    courseViewModels.Add(course);
                }
            }
            return courseViewModels;
        }
        public async Task<Team> GetTeamByClassId(string classid)
        {
            var _graphApiCallHelper = await RunAsync();

            return await _graphApiCallHelper.GetTeamByClassId(classid);
        }
        public async Task<Channel> GetDefaultChannel(string teamid)
        {
            var _graphApiCallHelper = await RunAsync();

            return await _graphApiCallHelper.GetDefaultChannel(teamid);
        }
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

    }
}

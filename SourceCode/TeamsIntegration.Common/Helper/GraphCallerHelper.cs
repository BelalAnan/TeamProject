using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TeamsIntegration.Common.Helper
{
    public class GraphCallerHelper
    {
        protected HttpClient HttpClient { get; private set; }
        protected GraphServiceClient GraphServiceClient { get; private set; }
        public GraphCallerHelper(GraphServiceClient graphServiceClient)
        {
            GraphServiceClient = graphServiceClient;
        }
        public GraphCallerHelper(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
        public async Task CreateTeam()
        {

            var team = new Team
            {
                Description = "To Test Sync Tool ",
                DisplayName = "Test",
                Members = new TeamMembersCollectionPage() {
                  new AadUserConversationMember{
                  
                      Roles = new List<String>(){ "owner"},
                      AdditionalData = new Dictionary<string, object>(){ {"user@odata.bind", "https://graph.microsoft.com/v1.0/users('admin.teams@edu-worx.net')"}}
                  }
                },
                AdditionalData = new Dictionary<string, object>() { { "template@odata.bind", "https://graph.microsoft.com/v1.0/teamsTemplates('standard')" } }
            };

            await GraphServiceClient.Teams.Request().AddAsync(team);

            Console.WriteLine(string.Format("---First Team Created"));
        }

        public async Task<IEducationRootClassesCollectionPage> GetEducationalClasses()
        {
            return await GraphServiceClient.Education.Classes
            .Request()
             .GetAsync();

        }
        public async Task<Team> GetTeamByClassId(string ClassId)
            {
           return await GraphServiceClient.Teams[ClassId]
	.Request()
	.GetAsync();
            }
        public async Task<Channel> GetDefaultChannel(string TeamId)
        {
            var channels = await GraphServiceClient.Teams[TeamId].Channels
            .Request()
            .GetAsync();
            return channels.Where(x => x.DisplayName == "General").FirstOrDefault();
        }

        
    }
}

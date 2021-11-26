using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TeamsIntegration.SyncTool.ViewModels;

namespace TeamsIntegration.SyncTool
{
    public class LMSBusinessLayer : ILMSBusinessLayer
    {
        public async Task AddCourseTeamChannel(CourseTeamChannelvm courseTeamChannelvm)
        {
            using (var httpClient = new HttpClient())
            {
                var courseteamchanneljson = JsonConvert.SerializeObject(courseTeamChannelvm);
             var res=   await httpClient.PostAsync("http://localhost:3000/CourseTeamChannel", new StringContent(courseteamchanneljson, Encoding.UTF8, "application/json"));
               
               if(!res.IsSuccessStatusCode)
                {
                    //logging error 
                }

            }
        }

        public async Task<List<CourseViewModel>> GetLMSCourses()
        {
            var CoursesList = new List<CourseViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:3000/LMSCourses"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    CoursesList = JsonConvert.DeserializeObject<List<CourseViewModel>>(apiResponse);
                }
            }
            return CoursesList;
        }

    }
}

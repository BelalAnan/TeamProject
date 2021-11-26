using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamsIntegration.SyncTool.ViewModels;

namespace TeamsIntegration.SyncTool.Business
{
    public interface ITeamBusinessLayer
    {
        Task<Channel> GetDefaultChannel(string teamid);
        Task<List<EducationClassViewModel>> GetEducationalCourses();
        Task<Team> GetTeamByClassId(string classid);
    }
}
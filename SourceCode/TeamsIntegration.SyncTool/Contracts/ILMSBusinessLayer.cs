using System.Collections.Generic;
using System.Threading.Tasks;
using TeamsIntegration.SyncTool.ViewModels;

namespace TeamsIntegration.SyncTool
{
    public interface ILMSBusinessLayer
    {
        Task<List<CourseViewModel>> GetLMSCourses();
        Task AddCourseTeamChannel(CourseTeamChannelvm courseTeamChannelvm);
    }
}
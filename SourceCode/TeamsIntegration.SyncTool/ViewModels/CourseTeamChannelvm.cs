using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsIntegration.SyncTool.ViewModels
{
   public class CourseTeamChannelvm
    {
        public Guid ClassId { get; set; }
        public string TeamId { get; set; }
        public string ChannelId { get; set; }
        public string TenantId { get; set; }

    }
}

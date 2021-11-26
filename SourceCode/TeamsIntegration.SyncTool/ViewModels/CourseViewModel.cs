using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsIntegration.SyncTool.ViewModels
{
   public class CourseViewModel
    {
        public Guid CourseId { get; set; }
        public string CourseExternalId { get; set; }
        public Guid ClassId { get; set; }
        public string ClassExternalId { get; set; }
    }
}

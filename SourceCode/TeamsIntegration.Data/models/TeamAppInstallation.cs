using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsIntegration.Data.models
{
 public   class TeamAppInstallation
    {
        public Guid TeamId { get; set; }
        public string AppId { get; set; }
        public bool IsInstalled { get; set; }
    }
}

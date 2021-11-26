using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsIntegration.Common.Configuration
{
    public class Organizations
    {
        public string loginToken { get; set; }
        public string TokenUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Scope { get; set; }
        public List<Tenant> Tenants { get; set; }
    
    }
}

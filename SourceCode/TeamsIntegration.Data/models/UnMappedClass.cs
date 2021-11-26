using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsIntegration.Data.models
{
   public class UnMappedClass
    {
        [Key]
        public Guid ExternalClassId { get; set; }
        public string Tenants { get; set; }
        public DateTime LastCheckedDate { get; set; }
    }
}

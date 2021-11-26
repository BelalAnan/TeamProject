using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsIntegration.SyncTool.Business
{
    public class MappingBusiness : IMappingBusiness
    {
        private readonly ITeamBusinessLayer _teamBusinessLayer;
        private readonly ILMSBusinessLayer _lMSBusinessLayer;
        public MappingBusiness(ITeamBusinessLayer teamBusinessLayer, ILMSBusinessLayer lMSBusinessLayer)
        {
            _teamBusinessLayer = teamBusinessLayer;
            _lMSBusinessLayer = lMSBusinessLayer;
        }
        public async Task CheckMappingLMSandEducational()
        {
            var educationvm = await _teamBusinessLayer.GetEducationalCourses();
            var lmsvm = await _lMSBusinessLayer.GetLMSCourses();
            var mappedcourses = educationvm.Where(educationcourse => lmsvm.Any(c => c.ClassExternalId == educationcourse.ExternalId)).ToList();
            if (mappedcourses.Count == 0)
            {
                //should try another tenant
                // if empty should throw data in unmappedclass table
            }
            else
            {


            }


        }

    }
}

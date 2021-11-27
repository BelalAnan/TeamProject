using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsIntegration.Data.models;
using TeamsIntegration.SyncTool.Services.Contracts;

namespace TeamsIntegration.SyncTool.Business
{
    public class MappingBusiness : IMappingBusiness
    {
        private readonly ITeamBusinessLayer _teamBusinessLayer;
        private readonly ILMSBusinessLayer _lMSBusinessLayer;
        private readonly IAsyncRepository<TeamAppInstallation> _teamapprep;
        private readonly IAsyncRepository<UnMappedClass> _unmappedrepo;
        public MappingBusiness(ITeamBusinessLayer teamBusinessLayer, ILMSBusinessLayer lMSBusinessLayer, IAsyncRepository<TeamAppInstallation> teamapprep, IAsyncRepository<UnMappedClass> unmappedrepo)
        {
            _teamBusinessLayer = teamBusinessLayer;
            _lMSBusinessLayer = lMSBusinessLayer;
            _teamapprep = teamapprep;
            _unmappedrepo = unmappedrepo;
        }
        public async Task CheckMappingLMSandEducational()
        {
            
            await _teamapprep.AddAsync(new TeamAppInstallation()
            {
                 AppId="111-2222-3333-4444",
                 TeamId = new Guid(),
                IsInstalled =false

            });
            var educationvm = await _teamBusinessLayer.GetEducationalCourses();
            var lmsvm = await _lMSBusinessLayer.GetLMSCourses();
            var mappedcourses = educationvm.Where(educationcourse => lmsvm.Any(c => c.ClassExternalId == educationcourse.ExternalId)).ToList();
            if (mappedcourses.Count == 0)
            {
                //should try another tenant
                // if empty should throw data in unmappedclass table
              await  _teamapprep.ListAllAsync();
                await _unmappedrepo.ListAllAsync();

            }
            else
            {


            }


        }

    }
}

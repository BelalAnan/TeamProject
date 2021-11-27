using Microsoft.Extensions.DependencyInjection;
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
        private IAsyncRepository<TeamAppInstallation> _teamapprep;
        private readonly IAsyncRepository<UnMappedClass> _unmappedrepo;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MappingBusiness(ITeamBusinessLayer teamBusinessLayer, ILMSBusinessLayer lMSBusinessLayer,/* IAsyncRepository<TeamAppInstallation> teamapprep, IAsyncRepository<UnMappedClass> unmappedrepo, */IServiceScopeFactory serviceScopeFactory)
        {
           _serviceScopeFactory = serviceScopeFactory;
           // using var scope = _serviceScopeFactory.CreateScope();
            _teamBusinessLayer = teamBusinessLayer;
            _lMSBusinessLayer = lMSBusinessLayer;
          //  _teamapprep = teamapprep;
           // _unmappedrepo = unmappedrepo;
            //_teamapprep = teamapprep;
            //_unmappedrepo = unmappedrepo;
         //   _teamapprep = scope.ServiceProvider.GetRequiredService<IAsyncRepository<TeamAppInstallation>>();
          //  _unmappedrepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<UnMappedClass>>();

        }
        public async Task CheckMappingLMSandEducational()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                // You can ask for any service here and DI will resolve it and give you back service instance
                _teamapprep = scope.ServiceProvider.GetRequiredService<IAsyncRepository<TeamAppInstallation>>();

                await _teamapprep.AddAsync(new TeamAppInstallation()
                {
                    AppId = "111-2222-3333-4444",
                    TeamId = Guid.NewGuid(),
                    IsInstalled = false

                }); ;
            }
       
          //  var educationvm = await _teamBusinessLayer.GetEducationalCourses();
           // var lmsvm = await _lMSBusinessLayer.GetLMSCourses();
            //var mappedcourses = educationvm.Where(educationcourse => lmsvm.Any(c => c.ClassExternalId == educationcourse.ExternalId)).ToList();
            //if (mappedcourses.Count == 0)
            //{
                //should try another tenant
                // if empty should throw data in unmappedclass table
            //  await  _teamapprep.ListAllAsync();
              //  await _unmappedrepo.ListAllAsync();

            //}
            //else
            //{


            //}


        }

    }
}

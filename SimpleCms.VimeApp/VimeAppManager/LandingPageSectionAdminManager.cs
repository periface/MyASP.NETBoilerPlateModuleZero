using System;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using SimpleCms.VimeApp.Policies;
using SimpleCms.VimeApp.VimeAppEntities;

namespace SimpleCms.VimeApp.VimeAppManager
{
    public class LandingPageSectionAdminManager : DomainService, ILandingPageSectionAdminManager
    {
        private readonly IVimeAppLandingCreationPolicy _appLandingCreationPolicy;
        private readonly IRepository<LandingPage, Guid> _landingPageRepository;
        public LandingPageSectionAdminManager(IVimeAppLandingCreationPolicy appLandingCreationPolicy, IRepository<LandingPage, Guid> landingPageRepository)
        {
            _appLandingCreationPolicy = appLandingCreationPolicy;
            _landingPageRepository = landingPageRepository;
        }

        public void CreateLandingPageConfiguration(LandingPage configuration)
        {
            _appLandingCreationPolicy.CheckAttemptOfConfigCreation(configuration);
            _landingPageRepository.Insert(configuration);
        }

        public void AddSectionToConfig(LandingPageSection section, Guid idConfig)
        {
            _appLandingCreationPolicy.CheckAttemptOfSectionCreation(section);
            var config = _landingPageRepository.Get(idConfig);
            config.AddLandingPageSectionToConfig(section);
            _landingPageRepository.Update(config);
        }
    }
}

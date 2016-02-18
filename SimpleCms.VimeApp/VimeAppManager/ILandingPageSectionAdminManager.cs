using System;
using Abp.Domain.Services;
using SimpleCms.VimeApp.VimeAppEntities;

namespace SimpleCms.VimeApp.VimeAppManager
{
    public interface ILandingPageSectionAdminManager : IDomainService
    {
        void CreateLandingPageConfiguration(LandingPage configuration);
        void AddSectionToConfig(LandingPageSection section,Guid idConfig);
        
    }
}

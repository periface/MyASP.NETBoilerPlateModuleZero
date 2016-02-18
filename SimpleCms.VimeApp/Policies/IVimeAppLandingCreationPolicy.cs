using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;
using SimpleCms.VimeApp.VimeAppEntities;

namespace SimpleCms.VimeApp.Policies
{
    public interface IVimeAppLandingCreationPolicy : IDomainService
    {
        void CheckAttemptOfConfigCreation(LandingPage landingPage);
        void CheckAttemptOfSectionCreation(LandingPageSection section);
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using SimpleCms.ModuleCms.Entities;
using SimpleCms.ModuleCms.Managers;

namespace SimpleCms.ModuleCms.Policies
{
    public class SiteConfigPolicies : ISiteConfigPolicies
    {

        public void CheckConfigCreationPolicy(SiteConfig config)
        {
            return;
        }

        public void CheckForAlreadyConfiguredSite()
        {
            return;
        }

        public void AttemptDeleteInfo(SiteInfo info)
        {
            if (info.IsActive)
            {

                throw new UserFriendlyException("Cannot delete if current info is active!");
            }
        }
    }
}

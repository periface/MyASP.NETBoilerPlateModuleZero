using Abp.Application.Services;
using SimpleCms.ModuleCms.Entities;

namespace SimpleCms.ModuleCms.Policies
{
    public interface ISiteConfigPolicies : IApplicationService
    {
        void CheckConfigCreationPolicy(SiteConfig config);
        void CheckForAlreadyConfiguredSite();
        void AttemptDeleteInfo(SiteInfo info);
    }
}

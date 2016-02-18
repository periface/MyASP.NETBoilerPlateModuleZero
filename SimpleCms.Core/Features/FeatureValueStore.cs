using Abp.Application.Features;
using SimpleCms.Authorization.Roles;
using SimpleCms.MultiTenancy;
using SimpleCms.Users;

namespace SimpleCms.Features
{
    public class FeatureValueStore : AbpFeatureValueStore<Tenant, Role, User>
    {
        public FeatureValueStore(TenantManager tenantManager)
            : base(tenantManager)
        {
        }
    }
}
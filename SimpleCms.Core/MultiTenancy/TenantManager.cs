using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using SimpleCms.Authorization.Roles;
using SimpleCms.Editions;
using SimpleCms.Users;

namespace SimpleCms.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, Role, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager
            )
        {
        }
    }
}
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;
using SimpleCms.Authorization.Roles;
using SimpleCms.MultiTenancy;
using SimpleCms.Users;

namespace SimpleCms.ModuleZero.Managers
{
    public class RoleManager : AbpRoleManager<Tenant, Role, User>
    {

        public RoleManager(AbpRoleStore<Tenant, Role, User> store, IPermissionManager permissionManager, IRoleManagementConfig roleManagementConfig, ICacheManager cacheManager) : base(store, permissionManager, roleManagementConfig, cacheManager)
        {
        }


    }
}

using Abp.Authorization.Roles;
using SimpleCms.MultiTenancy;
using SimpleCms.Users;

namespace SimpleCms.Authorization.Roles
{
    public interface IRoleAppService
    {
    }

    public class Role : AbpRole<Tenant, User>, IRoleAppService
    {

    }
}
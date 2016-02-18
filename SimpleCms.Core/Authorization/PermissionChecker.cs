using Abp.Authorization;
using SimpleCms.Authorization.Roles;
using SimpleCms.MultiTenancy;
using SimpleCms.Users;

namespace SimpleCms.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}

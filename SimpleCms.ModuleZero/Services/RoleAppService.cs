using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using SimpleCms.ModuleZero.Managers;
using SimpleCms.ModuleZero.Outputs;
using SimpleCms.ModuleZero.UserInput;
using Role = SimpleCms.Authorization.Roles.Role;

namespace SimpleCms.ModuleZero.Services
{
    public class RoleAppService : ApplicationService, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IPermissionManager _permissionManager;
        public RoleAppService(RoleManager roleVimeManager, IPermissionManager permissionManager)
        {
            _roleManager = roleVimeManager;
            _permissionManager = permissionManager;
        }
        public async Task UpdateRolePermisions(int id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var permissions = _permissionManager.GetAllPermissions().Where(a => "" == a.Name).ToList();
            await _roleManager.SetGrantedPermissionsAsync(role, permissions);
        }

        public async Task CreateRole(NewRoleInput roleInput)
        {
            var permissions = PermissionManager.GetAllPermissions().Where(a => roleInput.PermisionList.Contains(a.Name)).ToList();
            var role = new Role()
            {
                Name = roleInput.RoleName,
            };

            await _roleManager.CreateAsync(role);
            await _roleManager.SetGrantedPermissionsAsync(role.Id, permissions);
        }

        public async Task UpdateRolePermissions(UpdateRolesPermisionsInput roleInput)
        {
            var role = await _roleManager.GetRoleByIdAsync(roleInput.RoleId);
            var permissions =
                _permissionManager.GetAllPermissions().Where(a => roleInput.PermisionList.Contains(a.Name)).ToList();
            await _roleManager.SetGrantedPermissionsAsync(role, permissions);
        }

        public RoleListOutPut GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            var roleOutPut = new RoleListOutPut()
            {
                Roles = roles.Select(r => new Outputs.Role()
                {
                    RoleName = r.Name,
                    Id = r.Id,
                    Permissions = r.Permissions.Select(a => a.Name).ToList(),
                    Type = r.IsStatic ? "Static" : "Default",
                    CreationTime = r.CreationTime
                }).ToList(),

            };
            return roleOutPut;
        }

    }
}

using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Abp.Application.Services;
using SimpleCms.ModuleZero.Roles.Dto;
using SimpleCms.ModuleZero.Users.Dto;

namespace SimpleCms.ModuleZero.Roles
{
    public interface IRoleAppServiceZero : IApplicationService
    {
        Task CreateRole(NewRoleInput roleInput);
        Task UpdateRolePermissions(UpdateRolesPermisionsInput roleInput);
        Task AssignPermissions(List<string> permissions, string name);
        RoleListOutPut GetRoles();
        Task<NewRoleInput> GetRole(int id);
        List<Permissions> GetAllPermissions();
        Task<List<Permissions>> GetAssignedPermissions(int id);
        Task EditRole(NewRoleInput roleInput);
        Task DeleteRole(DeleteRoleInput input);
        List<RoleInput> GetAllRoles();
        Task<List<RoleInput>> GetAllRolesFromUser(long userId);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using SimpleCms.ModuleZero.Outputs;
using SimpleCms.ModuleZero.UserInput;

namespace SimpleCms.ModuleZero.Services
{
    public interface IRoleAppServiceZero : IApplicationService
    {
        Task CreateRole(NewRoleInput roleInput);
        Task UpdateRolePermissions(UpdateRolesPermisionsInput roleInput);
        Task AssignPermissions(List<string> permissions,string name);
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

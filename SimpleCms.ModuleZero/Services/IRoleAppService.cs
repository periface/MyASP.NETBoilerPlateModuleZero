using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using SimpleCms.ModuleZero.Outputs;
using SimpleCms.ModuleZero.UserInput;

namespace SimpleCms.ModuleZero.Services
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermisions(int id);
        Task CreateRole(NewRoleInput roleInput);
        Task UpdateRolePermissions(UpdateRolesPermisionsInput roleInput);
        RoleListOutPut GetRoles();
    }
}

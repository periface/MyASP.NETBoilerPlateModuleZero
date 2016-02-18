using System.Threading.Tasks;
using Abp.Application.Services;
using SimpleCms.Roles.Dto;

namespace SimpleCms.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}

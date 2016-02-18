using System.Threading.Tasks;
using Abp.Application.Services;
using SimpleCms.Users.Dto;

namespace SimpleCms.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);
    }
}
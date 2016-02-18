using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using SimpleCms.ModuleZero.GenericOutPuts;
using SimpleCms.ModuleZero.Users.Dto;

namespace SimpleCms.ModuleZero.Users
{
    public interface IUserAppServiceZero : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);
        Task AddUserToOrganizationUnit(int idUser, long orgId);
        Task RemoveUserFromOrganizationUnit(int idUser, long orgId);
        Task<UserOutPut> GetUserFromOrganizationUnit(long orgId);
        UserOutPut GetUsers();
        UserOutPut GetUsers(string searchString,int? rows,int?page,string order);
        JqGridObject GetUsersJqGridObject(string searchString, int? rows, int? page,string sortCol, string sort = "asc");
        Task CreateUser(NewUserInput input);
        Task<NewUserInput> GetUser(long id);
        Task AddImageToUser(HttpPostedFileBase image, long userId);
        Task EditUser(NewUserInput input);
        Task DeleteUser(long userId);
    }
}

using System.Threading.Tasks;
using Abp.Application.Services;

namespace SimpleCms.ModuleZero.Helpers
{
    public interface IAjaxPermissionHelper : IApplicationService
    {
        Task CheckPermission(string permission);
    }
}

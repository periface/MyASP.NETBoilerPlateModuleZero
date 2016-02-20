using System.Threading.Tasks;
using Abp.Authorization;
using Abp.UI;

namespace SimpleCms.ModuleZero.Helpers
{
    public class AjaxPermissionHelper : SimpleCmsAppServiceBase, IAjaxPermissionHelper
    {
        private readonly IPermissionChecker _checker;

        public AjaxPermissionHelper(IPermissionChecker checker)
        {
            _checker = checker;
        }

        public async Task CheckPermission(string permission)
        {
            if (!await _checker.IsGrantedAsync(permission)) throw new UserFriendlyException("No authorized");
        }
    }
}
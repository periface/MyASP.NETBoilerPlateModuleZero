using System.Threading.Tasks;
using Abp.Authorization;
using Abp.UI;
using SimpleCms.ModuleZero.Helpers;

namespace SimpleCms.Web.Helpers
{
    public class AjaxPermissionHelper : IAjaxPermissionHelper
    {
        private readonly IPermissionChecker _checker;

        public AjaxPermissionHelper(IPermissionChecker checker)
        {
            _checker = checker;
        }
        /// <summary>
        /// Name of the permission to check ex: Admin.Permission.XYxy
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task CheckPermission(string permission)
        {
            if (!await _checker.IsGrantedAsync(permission)) throw new UserFriendlyException("No authorized");
        }
    }
}
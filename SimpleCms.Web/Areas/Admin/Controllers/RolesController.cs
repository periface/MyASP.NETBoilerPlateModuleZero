using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using SimpleCms.ModuleZero.Constants;
using SimpleCms.ModuleZero.Notifications;
using SimpleCms.ModuleZero.Roles;
using SimpleCms.ModuleZero.Roles.Dto;
using SimpleCms.ModuleZero.Services;
using SimpleCms.Web.Controllers;

namespace SimpleCms.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize("Administration.ManageRoles")]
    public class RolesController : AdminController
    {
        private readonly IRoleAppServiceZero _roleAppServiceZero;
        private readonly INotificationsService _notificationsService;
        public RolesController(IRoleAppServiceZero roleAppServiceZero, INotificationsService notificationsService)
        {
            _roleAppServiceZero = roleAppServiceZero;
            _notificationsService = notificationsService;
        }

        // GET: Admin/Roles
        public ActionResult Index(string role)
        {
            if (!string.IsNullOrEmpty(role))
            {
                ViewBag.role = role;
            }
            return View();
        }
        [WrapResult(false)]
        public JsonResult GetRoles()
        {
            var roles = _roleAppServiceZero.GetRoles();
            return Json(roles.Roles, JsonRequestBehavior.AllowGet);
        }

        public async Task<ViewResult> EditRole(int? id,string roleName)
        {
            if (id.HasValue && string.IsNullOrEmpty(roleName))
            {
                var role = await _roleAppServiceZero.GetRole(id.Value);
                role.PermisionList = await _roleAppServiceZero.GetAssignedPermissions(id.Value);
                return View(role);
            }
            if (!string.IsNullOrEmpty(roleName) && !id.HasValue)
            {
                var role =await _roleAppServiceZero.GetRoleByName(roleName);
                return View(role);
            }
            throw new UserFriendlyException("Not found");
        }
        [HttpPost]
        public async Task<JsonResult> EditRole([Bind(Exclude = "TenantId")]NewRoleInput input)
        {
            input.TenantId = AbpSession.TenantId;
            await _roleAppServiceZero.EditRole(input);
            return Json(new {ok = true}, JsonRequestBehavior.AllowGet);
        }
        public ViewResult CreateRole()
        {
            var permissions = _roleAppServiceZero.GetAllPermissions();
            var roleModel = new NewRoleInput()
            {
                PermisionList = permissions
            };

            return View(roleModel);
        }

        public async Task<JsonResult> DeleteRole([Bind(Exclude = "TenantId")]DeleteRoleInput input)
        {
            input.TenantId = AbpSession.TenantId;
            await _roleAppServiceZero.DeleteRole(input);
            return Json(new {ok = true}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> CreateRole([Bind(Exclude = "TenantId")]NewRoleInput input)
        {
            input.TenantId = AbpSession.TenantId;
            input.UserId = AbpSession.UserId;
            await _roleAppServiceZero.CreateRole(input);
            await _roleAppServiceZero.AssignPermissions(input.CreatePermissions(),input.RoleName);
            return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
        }

        
    }
}
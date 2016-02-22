using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Runtime.Session;
using Abp.Threading;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using SimpleCms.ModuleZero.OrgUnits;
using SimpleCms.ModuleZero.OrgUnits.Dto;
using SimpleCms.ModuleZero.Services;
using SimpleCms.ModuleZero.Users;
using SimpleCms.ModuleZero.Users.Dto;
using SimpleCms.Web.Controllers;
using SimpleCms.Web.Helpers;

namespace SimpleCms.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize("Administration.OrgUnits")]
    public class OrgUnitsController : AdminController
    {
        private readonly IOrganizationUnitService _organizationUnitService;
        private readonly IUserAppServiceZero _userAppService;
        private readonly IAjaxPermissionHelper _ajaxPermissionHelper;
        public OrgUnitsController(IOrganizationUnitService organizationUnitService, IUserAppServiceZero userAppService, IAjaxPermissionHelper ajaxPermissionHelper)
        {
            _organizationUnitService = organizationUnitService;
            _userAppService = userAppService;
            _ajaxPermissionHelper = ajaxPermissionHelper;
        }
        [AbpMvcAuthorize("Administration.OrgUnits")]
        // GET: Admin/OrgUnits
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Create(OrganizationUnitInput input)
        {
            await _ajaxPermissionHelper.CheckPermission("Administration.OrgUnits.Create");
            if (!TryValidateModel(input)) { throw new UserFriendlyException(); }
            input.TenantId = AbpSession.TenantId;
            var id = await _organizationUnitService.CreateUnitGetId(input);
            return Json(new { createdId = id, Mensaje = "¡Unidad Creada!" });
        }
        [AbpMvcAuthorize("Administration.OrgUnits.Modify")]
        [HttpPost]
        public async Task<JsonResult> ChangeOrderOfUnits(List<OrderOrganizationUnitInput> input)
        {
            await _organizationUnitService.UpdateUnitOrder(input);
            return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
        }
        public ViewResult TreeView()
        {
            var units = AsyncHelper.RunSync(() => _organizationUnitService.LoadOrganizationUnitsWithChildren());
            return View("_orgUnitTree", units);
        }
        [AbpMvcAuthorize("Administration.OrgUnits.Create")]
        public ViewResult AddChild(long? id)
        {
            if (!id.HasValue) throw new UserFriendlyException("Area no encontrada");
            ViewBag.ParentId = id;
            return View();
        }
        [AbpMvcAuthorize("Administration.OrgUnits.Edit")]
        public async Task<ViewResult> EditUnit(long? id)
        {
            if (id == null) return View("Error");
            var unit = await _organizationUnitService.GetOrganizationUnit(id.Value);
            return View(unit);
        }

        [HttpPost]
        public async Task<JsonResult> EditUnit(OrganizationUnitDto model)
        {

            ValidateModel(model);
            await _organizationUnitService.EditUnit(model);
            return Json(new { ok = true });
        }
        [HttpPost]
        [AbpMvcAuthorize("Administration.OrgUnits.Delete")]
        public async Task<JsonResult> DeleteUnit(DeleteUnitInputDto input)
        {
            await _organizationUnitService.DeleteUnit(input.Id);
            return Json(new { ok = true });
        }
        [AbpMvcAuthorize("Administration.ManageUsers.ViewUsers")]
        [WrapResult(false)]
        public async Task<JsonResult> UsersFromUnit(long? id, int? rows, int? page, string sort = "asc")
        {
            if (id == null) return Json(new UserOutPut() { Users = new List<UserDto>() });
            var users = await _userAppService.GetUserFromOrganizationUnit(id.Value);
            return Json(users.Users, JsonRequestBehavior.AllowGet);
        }
        [AbpMvcAuthorize("Administration.OrgUnits.Modify")]
        public async Task<JsonResult> AddUserToUnit(UserToUnitInput input)
        {
            await _userAppService.AddUserToOrganizationUnit(input.IdUser, input.IdUnit);
            return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
        }
        [AbpMvcAuthorize("Administration.ManageUsers.ViewUsers")]
        public ViewResult LoadAllUsers(int id)
        {
            ViewBag.OrgUnitId = id;
            return View();
        }
        [HttpPost]
        [AbpMvcAuthorize("Administration.OrgUnits.Modify")]
        public async Task<JsonResult> RemoveFromUnit(NewUserToOrganization input)
        {
            await _userAppService.RemoveUserFromOrganizationUnit(input.IdMember, input.IdOrganizationSection);
            return Json(new { ok = true });
        }
        [WrapResult(false)]
        public JsonResult AllUsers(string searchString, int? rows, int? page, string sort = "asc")
        {
            var users = _userAppService.GetUsersJqGridObject(searchString, rows, page, sort);
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> ConvertToRoot(OrganizationUnitDto input)
        {
            await _organizationUnitService.TurnToRoot(input);
            return Json(new { ok = true });
        }
        
    }
}
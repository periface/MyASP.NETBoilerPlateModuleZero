using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using SimpleCms.ModuleZero.Roles;
using SimpleCms.ModuleZero.Services;
using SimpleCms.ModuleZero.Users;
using SimpleCms.ModuleZero.Users.Dto;
using SimpleCms.Web.Controllers;

namespace SimpleCms.Web.Areas.Admin.Controllers
{
    
    public class UsersController : AdminController
    {
        private readonly IUserAppServiceZero _userAppServiceZero;
        private readonly IRoleAppServiceZero _roleAppServiceZero;
        public UsersController(IUserAppServiceZero userAppServiceZero, IRoleAppServiceZero roleAppServiceZero)
        {
            _userAppServiceZero = userAppServiceZero;
            _roleAppServiceZero = roleAppServiceZero;
        }
        
        // GET: Admin/Users
        public virtual ActionResult Index()
        {
            return View();
        }
        [WrapResult(false)]
        public virtual JsonResult GetUsers(string searchString, int? rows, int? page, string sidx, string sord = "asc")
        {
            var model = _userAppServiceZero.GetUsersJqGridObject(searchString, rows, page, sidx, sord);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [AbpMvcAuthorize("Administration.ManageUsers.Create")]
        public virtual ActionResult CreateUser()
        {
            var user = new NewUserInput()
            {
                Roles = _roleAppServiceZero.GetAllRoles(),
                SendActivationEmail = true,
                IsActive = true,
                CreateRandomPassword = true
            };
            return View(user);
        }
        [HttpPost]
        [AbpMvcAuthorize("Administration.ManageUsers.Create")]
        public virtual async Task<ActionResult> CreateUser(NewUserInput input)
        {
            await _userAppServiceZero.CreateUser(input);
            return Json(new { ok = true });
        }

        public List<RoleInput> Roles
        {
            get
            {
                var roles = new List<RoleInput>
                {
                    new RoleInput()
                    {
                        Granted = true,
                        RoleName = "Role"
                    },
                    new RoleInput()
                    {
                        Granted = false,
                        RoleName = "Role 2"
                    }
                };
                return roles;
            }
        }
        [AbpMvcAuthorize("Administration.ManageUsers.Edit")]
        public virtual async Task<ActionResult> EditUser(long id)
        {
            var user = await _userAppServiceZero.GetUser(id);
            user.CreateRandomPassword = false;
            user.SendActivationEmail = false;
            user.Roles = await _roleAppServiceZero.GetAllRolesFromUser(id);
            return View(user);
        }

        [HttpPost]
        [AbpMvcAuthorize("Administration.ManageUsers.Edit")]
        public virtual async Task<ActionResult> EditUser(NewUserInput input)
        {
            await _userAppServiceZero.EditUser(input);
            return Json(new { ok = true });
        }
        public async Task<JsonResult> UploadImageForUser(long id)
        {
            if (Request.Files.Count <= 0) throw new UserFriendlyException("Image not found!");
            var image = Request.Files[0];
            await _userAppServiceZero.AddImageToUser(image, id);
            return null;
        }
        [AbpMvcAuthorize("Administration.ManageUsers.Delete")]
        public virtual async Task<JsonResult> DeleteUser(long id)
        {
            if (id == AbpSession.UserId)
            {
                throw new UserFriendlyException("Cannot delete current user!");
            }

            await _userAppServiceZero.DeleteUser(id);
            
            return null;
        }
    }
}
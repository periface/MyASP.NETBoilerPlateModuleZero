using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Abp.Threading;
using Abp.UI;
using Abp.Web.Mvc.Authorization;
using SimpleCms.ModuleCms.Inputs;
using SimpleCms.ModuleCms.SiteConfiguration;
using SimpleCms.ModuleCms.SiteConfiguration.Dto;
using SimpleCms.Web.Controllers;

namespace SimpleCms.Web.Areas.AdminCms.Controllers
{
    [AbpMvcAuthorize()]
    public class SiteConfigController : AdminController
    {
        private readonly ISiteService _siteService;

        public SiteConfigController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        // GET: Admin/SiteConfig
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult SiteInfos()
        {
            var infos = AsyncHelper.RunSync(() => _siteService.GetInfoList());
            return View(infos);
        }

        public ViewResult CreateInfo()
        {
            var info = new SiteInfoDto()
            {
                SiteTitle = "My Website",
                IsActive = false,
                SiteDescription = "Description",
                SiteSlogan = "My Slogan"
            };
            return View(info);
        }
        public async Task<ViewResult> EditInfo(int? id)
        {
            if (!id.HasValue) throw new MembershipCreateUserException("Invalid operation!");
            var info = await _siteService.GetCurrentInfoForEdit(id.Value);
            return View(info);
        }
        [HttpPost]
        public async Task<JsonResult> EditInfo(SiteInfoDto info)
        {
            ValidateModel(info);
            await _siteService.EditInfo(info);
            return Json(new { ok = true });
        }
        [HttpPost]
        public async Task<JsonResult> CreateInfo(SiteInfoDto info)
        {
            ValidateModel(info);
            await _siteService.CreateInfo(info);
            return Json(new { ok = true });
        }
        public ViewResult UploadImage(string discriminator, int id)
        {
            return View(new SiteInfoImageInput()
            {
                Discriminator = discriminator,
                IdConfig = id,
            });
        }

        public async Task<JsonResult> UploadImageToInfo(string discriminator,int id)
        {
            if (Request.Files.Count <= 0) throw new UserFriendlyException("No image uploaded");
            await _siteService.AddImageToInfo(new SiteInfoImageInput()
            {
                Discriminator = discriminator,
                IdConfig = id,
                Image = Request.Files[0]
            });
            return Json(new { ok = true });
        }

        public async Task<ActionResult> SetInfoAsActive(int id)
        {
            await _siteService.ActivateInfo(id);
            return Json(new {ok = true});
        }

        public async Task<JsonResult> DeleteInfo(int id)
        {
            await _siteService.DeleteInfo(id);
            return Json(new {ok = true});
        }
    }
}
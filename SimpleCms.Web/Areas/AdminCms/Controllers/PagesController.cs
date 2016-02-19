using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using SimpleCms.ModuleCms.Pages;
using SimpleCms.Web.Controllers;

namespace SimpleCms.Web.Areas.AdminCms.Controllers
{
    [AbpMvcAuthorize()]
    public class PagesController : AdminController
    {
        private readonly IPageService _pageService;

        public PagesController(IPageService pageService)
        {
            _pageService = pageService;
        }

        // GET: Admin/Pages
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult GetCategories(string langName)
        {
            var categories = _pageService.GetOnlyCategories(langName);
            return View(categories);
        }
    }
}
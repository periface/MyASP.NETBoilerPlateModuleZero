using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using SimpleCms.Web.Controllers;

namespace SimpleCms.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize()]
    public class PagesController : SimpleCmsControllerBase
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            return View();
        }
    }
}
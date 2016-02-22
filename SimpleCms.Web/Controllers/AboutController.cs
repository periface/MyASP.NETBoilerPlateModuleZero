using System.Web.Mvc;
using Abp.Authorization;

namespace SimpleCms.Web.Controllers
{
    public class AboutController : SimpleCmsControllerBase
    {
        public ActionResult Index()
        {
            ViewBag.Title = "About";
            return View();
        }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleCms.Web.Controllers;

namespace SimpleCms.Web.Areas.Admin.Controllers
{
    public class AdministrationController : AdminController
    {
        // GET: Admin/Administration
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Roles()
        {
            return View();
        }
    }
}
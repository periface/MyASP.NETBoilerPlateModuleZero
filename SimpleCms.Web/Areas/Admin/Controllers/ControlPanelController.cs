using System.Web.Mvc;
using Abp.Web.Models;
using SimpleCms.ModuleZero.Services;

namespace SimpleCms.Web.Areas.Admin.Controllers
{
    public class ControlPanelController : AdminController
    {
        private readonly IAuditLogService _auditLogService;

        public ControlPanelController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        // GET: Admin/ControlPanel
        public ActionResult Index()
        {
            return View();
        }
        [WrapResult(false)]

        public ActionResult GetAuditLogs()
        {
            if (AbpSession.TenantId == null) return null;
            var model = _auditLogService.GetAuditLogsFromTenant(AbpSession.TenantId.Value);
            return Json(model,JsonRequestBehavior.AllowGet);
        }
    }
}
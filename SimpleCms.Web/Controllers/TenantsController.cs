using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Threading;
using Abp.Web.Mvc.Authorization;
using SimpleCms.Authorization;
using SimpleCms.ModuleZero.LanguageTexts;
using SimpleCms.ModuleZero.Tenancy;
using SimpleCms.MultiTenancy;

namespace SimpleCms.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantsController : SimpleCmsControllerBase
    {
        private readonly ITenantAppService _tenantAppService;
        private readonly ILanguageService _languageService;
        private readonly ITenancyService _tenancyService;
        public TenantsController(ITenantAppService tenantAppService, ILanguageService languageService, ITenancyService tenancyService)
        {
            _tenantAppService = tenantAppService;
            _languageService = languageService;
            _tenancyService = tenancyService;
        }
        public ActionResult Index()
        {
            var output = _tenantAppService.GetTenants();
            return View(output);
        }
        private int? TenantId(string activeTenantName)
        {
            return AsyncHelper.RunSync(() => _tenancyService.GetTenantByName(activeTenantName));
        }
    }
}
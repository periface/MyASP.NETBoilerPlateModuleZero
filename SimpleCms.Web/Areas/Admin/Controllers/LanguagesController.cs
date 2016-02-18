using System.Globalization;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Threading;
using Abp.Web.Models;
using SimpleCms.ModuleZero.LanguageTexts;
using SimpleCms.ModuleZero.LanguageTexts.Dto;
using SimpleCms.ModuleZero.Tenancy;
using SimpleCms.Web.Controllers;

namespace SimpleCms.Web.Areas.Admin.Controllers
{
    public class LanguagesController : SimpleCmsControllerBase
    {
        private readonly ILanguageService _languageService;
        private readonly ITenancyService _tenancyService;
        public LanguagesController(ILanguageService languageService, ITenancyService tenancyService)
        {
            _languageService = languageService;
            _tenancyService = tenancyService;
        }

        // GET: Admin/Languages
        public ActionResult Index()
        {
            return View();
        }
        [WrapResult(false)]
        public async Task<JsonResult> LoadLanguages()
        {
            var tenancy = await _tenancyService.GetTenantByName(ActiveTenantName);
            var data = await _languageService.GetLanguages((int)tenancy);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateLanguage()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateLanguage(NewLanguageInputDto input)
        {
            CheckModelState();
            input.DisplayText = new CultureInfo(input.Name).DisplayName;
            input.TenantId = TenantId;
            await _languageService.CreateLanguage(input);
            return Json(new {ok = true});
        }

        public ViewResult EditLanguageText(string langName,bool isHost=false)
        {
            if (isHost)
            {
                _languageService.InitLanguages(TenantId,langName);
            }
            ViewBag.LangName = langName;
            return View();
        }

        public async Task<JsonResult> GetTenancyLanguages(string activeLang)
        {
            var languages = await _languageService.GetLanguages(TenantId,activeLang);
            return Json(languages);
        }
        [WrapResult(false)]
        public JsonResult GetLanguagesForEdit(string langTarget, string langBase, string langSource)
        {
            var model = _languageService.GetLanguageText(langTarget,langBase,langSource);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public async Task<ViewResult> EditLanguage(int id)
        {
            var language = await _languageService.GetLanguageForEdit(id);
            return View(language);
        }

        [HttpPost]
        public async Task<JsonResult> EditLanguage(NewLanguageInputDto input)
        {
            input.TenantId = TenantId;
            await _languageService.EditLanguage(input);
            return Json(new {ok = true});
        }
        private int? TenantId
        {
            get { return AsyncHelper.RunSync(() => _tenancyService.GetTenantByName(ActiveTenantName)); }
        }

        public ViewResult EditText(GetLanguageForEditInput input)
        {
            var text = _languageService.GetLanguageTextForEdit(input);
            return View(text);
        }
        [HttpPost]
        public async Task<JsonResult> EditTextValue(ApplicationTextInput input)
        {
            input.TenantId = TenantId;
            await _languageService.EditText(input);
            return Json(input,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNext(int id)
        {
            return new JsonResult();
        }
    }
}
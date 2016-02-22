using System.Globalization;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Models;
using SimpleCms.ModuleZero.LanguageTexts;
using SimpleCms.ModuleZero.LanguageTexts.Dto;

namespace SimpleCms.Web.Areas.Admin.Controllers
{
    public class LanguagesController : AdminController
    {
        private readonly ILanguageService _languageService;

        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        // GET: Admin/Languages
        public ActionResult Index()
        {
            return View();
        }
        [WrapResult(false)]
        public async Task<JsonResult> LoadLanguages()
        {
            var data = await _languageService.GetLanguages(AbpSession.TenantId);
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
            input.TenantId = AbpSession.TenantId;
            await _languageService.CreateLanguage(input);
            return Json(new {ok = true});
        }

        public ViewResult EditLanguageText(string langName,bool isHost=false)
        {
            //if (isHost)
            //{
            //    _languageService.InitLanguages(TenantId,langName);
            //}
            ViewBag.LangName = langName;
            return View();
        }

        public async Task<JsonResult> GetTenancyLanguages(string activeLang)
        {
            var languages = await _languageService.GetLanguages(AbpSession.TenantId,activeLang);
            return Json(languages);
        }
        [WrapResult(false)]
        public JsonResult GetLanguagesForEdit(string langTarget, string langBase, string langSource, string searchString, int? rows, int? page, string sidx, string sord = "asc")
        {
            var model = _languageService.GetLanguageText(langName:langTarget,baseLanguage:langBase,langSource:langSource,searchString:searchString,rows:rows,page:page,sortColumn:sidx,sortOrder:sord);
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
            input.TenantId = AbpSession.TenantId;
            await _languageService.EditLanguage(input);
            return Json(new {ok = true});
        }
        
        public ViewResult EditText(GetLanguageForEditInput input)
        {
            var text = _languageService.GetLanguageTextForEdit(input);
            return View(text);
        }
        [HttpPost]
        public async Task<JsonResult> EditTextValue(ApplicationTextInput input)
        {
            input.TenantId = AbpSession.TenantId;
            await _languageService.EditText(input);
            return Json(input,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> EditTextValueGetNext(ApplicationTextInput input)
        {
            input.TenantId = AbpSession.TenantId;
            var next = await _languageService.EditTextGetNext(input);
            return next == null ? Json(new {ok = true}) : Json(next);
        }

        public ViewResult ReloadEditTextForm(GetLanguageForEditInput input)
        {
            var text = _languageService.GetNextLanguageTextForEdit(input);
            return View(text);
        }
    }
}
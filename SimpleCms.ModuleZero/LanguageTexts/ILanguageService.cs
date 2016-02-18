using System.Threading.Tasks;
using Abp.Application.Services;
using SimpleCms.ModuleZero.Constants;
using SimpleCms.ModuleZero.GenericOutPuts;
using SimpleCms.ModuleZero.LanguageTexts.Dto;

namespace SimpleCms.ModuleZero.LanguageTexts
{
    public interface ILanguageService : IApplicationService
    {
        Task<JqGridObject> GetLanguages(int? idTenancy);
        Task<LanguageOutPut> GetLanguages(int? idTenancy, string setActiveStatusName);
        Task UpdateEntry(ApplicationTextInput input);
        Task CreateLanguage(NewLanguageInputDto input);
        JqGridObject GetLanguageText(string langName, string baseLanguage = "en", string langSource = ModuleZeroConstants.Source);
        Task<NewLanguageInputDto> GetLanguageForEdit(int id);
        Task EditLanguage(NewLanguageInputDto input);
        Task EditText(ApplicationTextInput input);
        ApplicationTextInput GetLanguageTextForEdit(GetLanguageForEditInput input);
        Task InitLanguages(int? createdTenantId);
        Task InitLanguages(int? tenantId, string langName);
    }
}

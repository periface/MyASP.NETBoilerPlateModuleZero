using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Localization;
using SimpleCms.ModuleZero.LanguageTexts.Dto;

namespace SimpleCms.ModuleZero.LanguageTexts
{
    public class LanguajeService : ILanguageService
    {
        private readonly IApplicationLanguageManager _languageManager;
        private readonly IApplicationLanguageTextManager _applicationLanguageTextManager;
        private readonly IRepository<ApplicationLanguage> _repositoryLanguages;
        private readonly IRepository<ApplicationLanguageText,long> _repositoryLanguageText;
        private LanguageManager _languageManagerSecondary;
        public LanguajeService(IApplicationLanguageManager languageManager, IApplicationLanguageTextManager applicationLanguageTextManager, LanguageManager languageManagerSecondary, IRepository<ApplicationLanguage> repositoryLanguages, IRepository<ApplicationLanguageText, long> repositoryLanguageText)
        {
            _languageManager = languageManager;
            _applicationLanguageTextManager = applicationLanguageTextManager;
            _languageManagerSecondary = languageManagerSecondary;
            _repositoryLanguages = repositoryLanguages;
            _repositoryLanguageText = repositoryLanguageText;
        }

        public async Task<IEnumerable<ApplicationLanguage>> GetLanguages(int idTenant)
        {
            var languages = await _languageManager.GetLanguagesAsync(idTenant);
            return languages;
        }

        public async Task<List<string>> GetSourceNames(int idTenant)
        {
            var languages = await _repositoryLanguageText.GetAllListAsync(a => a.TenantId == idTenant);
            return languages.Select(a => a.Source).ToList();
        }
        public async Task UpdateEntry(ApplicationTextInput input)
        {
            await _applicationLanguageTextManager.UpdateStringAsync(input.TenantId, input.SourceName, input.CurrentCulture,
                input.Key, input.Value);
        }
    }
}

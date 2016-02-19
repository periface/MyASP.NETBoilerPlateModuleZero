using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.Runtime.Caching;
using Abp.UI;
using SimpleCms.ModuleZero.Constants;
using SimpleCms.ModuleZero.GenericOutPuts;
using SimpleCms.ModuleZero.LanguageTexts.Dto;

namespace SimpleCms.ModuleZero.LanguageTexts
{
    public class LanguageService : ILanguageService
    {
        private readonly IApplicationLanguageManager _languageManager;
        private readonly IApplicationLanguageTextManager _applicationLanguageTextManager;
        private readonly IRepository<ApplicationLanguageText, long> _repositoryLanguageText;
        private readonly IRepository<ApplicationLanguage> _repositoryLanguage;
        private readonly ICacheManager _cacheManager;
        private readonly ILocalizationManager _localizationManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public LanguageService(IApplicationLanguageManager languageManager, IApplicationLanguageTextManager applicationLanguageTextManager, IRepository<ApplicationLanguageText, long> repositoryLanguageText, ICacheManager cacheManager, ILocalizationManager localizationManager, IRepository<ApplicationLanguage> repositoryLanguage, IUnitOfWorkManager unitOfWorkManager)
        {
            _languageManager = languageManager;
            _applicationLanguageTextManager = applicationLanguageTextManager;
            _repositoryLanguageText = repositoryLanguageText;
            _cacheManager = cacheManager;
            _localizationManager = localizationManager;
            _repositoryLanguage = repositoryLanguage;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<NewLanguageInputDto> GetLanguageForEdit(int id)
        {
            var language = await _repositoryLanguage.GetAsync(id);
            if (language == null) throw new UserFriendlyException("Language not found.");
            return new NewLanguageInputDto()
            {
                Name = language.Name,
                Icon = language.Icon,
                DisplayText = language.DisplayName,
                TenantId = language.TenantId
            };
        }

        public async Task EditLanguage(NewLanguageInputDto input)
        {
            await _languageManager.UpdateAsync(input.TenantId, new ApplicationLanguage()
            {
                DisplayName = input.DisplayText,
                Name = input.Name,
                Icon = input.Icon,
            });
        }

        public async Task EditText(ApplicationTextInput input)
        {
            await _applicationLanguageTextManager.UpdateStringAsync(input.TenantId, input.SourceName, input.CurrentCulture, input.Key, input.Value);

        }

        public ApplicationTextInput GetLanguageTextForEdit(GetLanguageForEditInput input)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var baseLanguage = _repositoryLanguageText.FirstOrDefault(a => a.Key == input.Key && a.LanguageName == input.BaseLanguage && a.Source == input.SourceName);
                var langText = _repositoryLanguageText.FirstOrDefault(a => a.Key == input.Key && a.LanguageName == input.TargetLanguage && a.Source == input.SourceName);
                if (langText == null)
                {
                    return new ApplicationTextInput()
                    {
                        Key = baseLanguage.Key,
                        SourceName = baseLanguage.Source,
                        Value = "",
                        Info = new OutPutInfo()
                        {
                            BaseLanguage = baseLanguage.LanguageName,
                            BaseValue = baseLanguage.Value,
                            TargetLanguage = input.TargetLanguage,
                        }
                    };
                }
                return new ApplicationTextInput()
                {
                    Key = langText.Key,
                    SourceName = langText.Source,
                    Value = langText.Value,
                    Info = new OutPutInfo()
                    {
                        BaseLanguage = baseLanguage.LanguageName,
                        BaseValue = baseLanguage.Value,
                        TargetLanguage = langText.LanguageName,
                    }
                };
            }
        }

        public async Task<JqGridObject> GetLanguages(int? idTenant)
        {
            var dtoModel = new LanguageOutPut();
            var languages = await _languageManager.GetLanguagesAsync(idTenant);
            foreach (var applicationLanguage in languages)
            {
                dtoModel.Languages.Add(new LanguageDto()
                {
                    CreationDate = applicationLanguage.CreationTime.ToShortDateString(),
                    DisplayName = applicationLanguage.DisplayName,
                    Icon = applicationLanguage.Icon,
                    Id = applicationLanguage.Id,
                    Name = applicationLanguage.Name,
                    IsHost = !applicationLanguage.TenantId.HasValue,
                    State = applicationLanguage.TenantId.HasValue ? _localizationManager.GetString(ModuleZeroConstants.Source, "NoStatic") : _localizationManager.GetString(ModuleZeroConstants.Source, "Static")
                });
            }
            var model = JqGridObject.CreateModel(1, 10, 100, "", "", dtoModel.Languages);
            return model;
        }

        public async Task<LanguageOutPut> GetLanguages(int? idTenant, string setActiveStatusName)
        {
            if (string.IsNullOrEmpty(setActiveStatusName)) setActiveStatusName = "en";
            var dtoModel = new LanguageOutPut();
            var languages = await _languageManager.GetLanguagesAsync(idTenant);
            foreach (var applicationLanguage in languages)
            {
                dtoModel.Languages.Add(new LanguageDto()
                {
                    CreationDate = applicationLanguage.CreationTime.ToShortDateString(),
                    DisplayName = applicationLanguage.DisplayName,
                    Icon = applicationLanguage.Icon,
                    Id = applicationLanguage.Id,
                    Name = applicationLanguage.Name,
                    IsActive = string.Equals(applicationLanguage.Name, setActiveStatusName, StringComparison.CurrentCultureIgnoreCase),
                    IsHost = !applicationLanguage.TenantId.HasValue,
                    State = applicationLanguage.TenantId.HasValue ? _localizationManager.GetString(ModuleZeroConstants.Source, "NoStatic") : _localizationManager.GetString(ModuleZeroConstants.Source, "Static")
                });
            }
            dtoModel.Languages = dtoModel.Languages.OrderByDescending(a => a.IsActive).ToList();
            return dtoModel;
        }

        /// <summary>
        /// Not used get
        /// </summary>
        /// <param name="idTenant"></param>
        /// <returns></returns>
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

        public async Task CreateLanguage(NewLanguageInputDto input)
        {

            var language = new ApplicationLanguage()
            {
                TenantId = input.TenantId,
                DisplayName = input.DisplayText,
                Name = input.Name,
                Icon = input.Icon
            };

            await _languageManager.AddAsync(language);

            //Todo: Dis crap mast be fixed!

            //await GetHostLanguages(input.TenantId, input.Name);


            _cacheManager.GetCache("AbpZeroLanguages").Clear();
            _cacheManager.GetCache("AbpZeroMultiTenantLocalizationDictionaryCache").Clear();
        }
        /// <summary>
        /// harmful code!
        /// </summary>
        /// <param name="createdTenantId"></param>
        /// <returns></returns>
        public async Task InitLanguages(int? createdTenantId)
        {
            var languages = _repositoryLanguageText.GetAllList(a => a.TenantId == null);
            foreach (var lang in _repositoryLanguage.GetAllList(a => a.TenantId == createdTenantId))
            {
                foreach (var applicationLanguageText in languages)
                {
                    await _applicationLanguageTextManager.UpdateStringAsync(createdTenantId, applicationLanguageText.Source, new CultureInfo(lang.Name),
                        applicationLanguageText.Key, "");
                }
            }
        }

        /// <summary>
        /// harmful code!
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="langName"></param>
        /// <returns></returns>
        public async Task InitLanguages(int? tenantId, string langName)
        {
            var languageTextsHost = GetHostLanguagesList(langName);
            var languageTextClient = _repositoryLanguageText.GetAllList(a => a.LanguageName == langName);
            var langEnum = languageTextsHost as ApplicationLanguageText[] ?? languageTextsHost.ToArray();

            foreach (var applicationLanguageText in langEnum)
            {
                if (languageTextClient.Any())
                {
                    // ReSharper disable once UnusedVariable
                    foreach (var languageText in languageTextClient.Where(languageText => applicationLanguageText.Value == languageText.Value))
                    {
                        await
                            _applicationLanguageTextManager.UpdateStringAsync(tenantId, applicationLanguageText.Source,
                                new CultureInfo(langName),
                                applicationLanguageText.Key, "");
                    }
                }
                else
                {
                    await _applicationLanguageTextManager.UpdateStringAsync(tenantId, applicationLanguageText.Source, new CultureInfo(langName),
                        applicationLanguageText.Key, applicationLanguageText.Value);
                }

            }
        }

        public JqGridObject GetLanguageText(string langName, string searchString, int? rows, int? page, string sortColumn, string sortOrder = "asc", string baseLanguage = "en", string source = ModuleZeroConstants.Source)
        {
            var pageIndex = page - 1 ?? 0;
            var pageSize = rows ?? 10;

            var totalRecords =0;
            //Gets the texts filtered from host 
            var hostTexts = FilterHostList(baseLanguage, source, searchString, pageIndex, pageSize, sortColumn, sortOrder,out totalRecords);
            
            
            //Gets the texts filtered from client
            var clientTexts = FilterClientList(langName, source, searchString, pageIndex, pageSize, sortColumn, sortOrder);

            if (!clientTexts.Any())
            {
                clientTexts.AddRange(hostTexts.Select(applicationLanguageText => new ApplicationLanguageText()
                {
                    Key = applicationLanguageText.Key, LanguageName = applicationLanguageText.LanguageName, Source = applicationLanguageText.Source
                }));
            }
            //Realizes the final filter
            var list = (from applicationLanguageText in hostTexts
                        from languageText in clientTexts
                        where applicationLanguageText.Key == languageText.Key
                        select new LanguageTextOutPutDto()
                        {
                            BaseValue = applicationLanguageText.Value,
                            Id = applicationLanguageText.Id,
                            TargetValue = languageText.Value,
                            Key = applicationLanguageText.Key
                        }).ToList();
            var dtoModel = new LanguageTextOutput()
            {
                LanguageText = list
            };
            
            var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);

            var model = JqGridObject.CreateModel(page ?? 1, totalRecords, totalPages, "", sortOrder, dtoModel.LanguageText);
            return model;
        }

        private List<ApplicationLanguageText> FilterClientList(string baseLanguage, string source, string searchString, int pageIndex, int pageSize, string sortColumn, string sortOrder)
        {
            var isAsc = sortOrder == "asc";
            var baseText = _repositoryLanguageText.GetAll();
            baseText = from text in baseText where text.LanguageName.ToUpper().Equals(baseLanguage) && source.ToUpper().Equals(source) select text;
           
            if (!string.IsNullOrEmpty(searchString))
            {
                baseText = from text in baseText where text.Key.Contains(searchString)select text;
            }
            var dataModel = baseText.OrderBy(a => a.Key).Skip(pageIndex * pageSize).ToList();
            return dataModel;
        }
        private List<ApplicationLanguageText> FilterHostList(string baseLanguage, string source, string searchString, int pageIndex, int pageSize, string sortColumn, string sortOrder,out int total)
        {
            var isAsc = sortOrder == "asc";
            
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var baseText = _repositoryLanguageText.GetAll().Where(a => a.TenantId == null);
                baseText = from text in baseText where text.LanguageName.ToUpper().Equals(baseLanguage) && source.ToUpper().Equals(source) select text;
                if (!string.IsNullOrEmpty(searchString))
                {
                    baseText = from text in baseText where text.Key.Contains(searchString)select text;
                };
                total = baseText.Count();
                var dataModel = baseText.OrderBy(a=>a.Key).Skip(pageIndex * pageSize).ToList();
                return dataModel;

            }

        }
        /// <summary>
        /// Asign the host default language texts to the provided language
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="languageName"></param>
        /// <returns></returns>
        private async Task GetHostLanguages(int? tenantId, string languageName)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                //
                var languages = _repositoryLanguageText.GetAllList(a => a.TenantId == null);
                //
                foreach (var applicationLanguageText in languages)
                {

                    await _applicationLanguageTextManager.UpdateStringAsync(tenantId, applicationLanguageText.Source, new CultureInfo(languageName),
                        applicationLanguageText.Key, "");
                }
            }
        }
        private IEnumerable<ApplicationLanguageText> GetHostLanguagesList(string languageName)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                //
                var languages = _repositoryLanguageText.GetAllList(a => a.TenantId == null && a.LanguageName == languageName);
                //
                return languages;
            }
        }
    }
}

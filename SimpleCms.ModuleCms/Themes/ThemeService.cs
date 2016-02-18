using System;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Threading;
using Abp.UI;
using SimpleCms.ModuleCms.Entities;
using SimpleCms.ModuleCms.Managers;
using SimpleCms.ModuleCms.Themes.Dto;
using SimpleCms.MultiTenancy;

namespace SimpleCms.ModuleCms.Themes
{
    public class ThemeService : IThemeService
    {
        private readonly IThemeManager _themeManager;
        private readonly ISiteManager _siteManager;
        private readonly IRepository<SiteConfig> _siteConfigRepository;
        private readonly IRepository<ConfigThemeRelation> _configThemeRelationRepository;
        private readonly TenantManager _tenantManager;
        public ThemeService(IThemeManager themeManager, ISiteManager siteManager, IRepository<SiteConfig> siteConfigRepository, IRepository<ConfigThemeRelation> configThemeRelationRepository, TenantManager tenantManager)
        {
            _themeManager = themeManager;
            _siteManager = siteManager;
            _siteConfigRepository = siteConfigRepository;
            _configThemeRelationRepository = configThemeRelationRepository;
            _tenantManager = tenantManager;
        }

        public ThemeOutput GetStoreThemes()
        {
            var themes = _themeManager.GetAvailableThemes();
            return new ThemeOutput()
            {
                Themes = themes.Select(a => new ThemeDto()
                {
                    Id = a.Id,
                    Image = a.ThemePreview,
                    Title = a.ThemeName,
                    Uses = GetGlobalThemeUses(a.Id),
                    IsAdquired = IsAlreadyAdquired(a.Id),
                    StillInDevelopment = a.IsUnderConstruction
                }).ToList()
            };
        }

        private bool IsAlreadyAdquired(int id)
        {
            var found = _configThemeRelationRepository.GetAllList(a => a.IdTheme == id);
            return found.Any();
        }
        public ThemeOutput GetTenantAsignedThemes()
        {
            var siteConfig = _siteManager.GetCurrentTenantConfig();
            return new ThemeOutput()
            {
                Themes = _themeManager.GetThemesFromConfig(siteConfig.Id).Select(a => new ThemeDto()
                {
                    Id = a.Id,
                    Image = a.ThemePreview,
                    IsAdquired = true,
                    StillInDevelopment = a.IsUnderConstruction,
                    Title = a.ThemeName,
                    Uses = GetGlobalThemeUses(a.Id),
                    IsActive = IsThisThemeActive(a.Id)
                }).ToList()
            };
        }

        private bool IsThisThemeActive(int id)
        {
            return _configThemeRelationRepository.GetAll().Any(a => a.IsActive && a.IdTheme == id);
        }

        public void AsignTheme(int idTheme)
        {
            throw new NotImplementedException();
        }

        public void AddComentToTheme(CommentThemeInput input)
        {
            throw new NotImplementedException();
        }

        public ThemeDto GetCurrentActiveThemeFromTenant()
        {
            var currentConfig = _siteManager.GetCurrentTenantConfig();
            var themes = _configThemeRelationRepository.GetAllList(a => a.IdConfig == currentConfig.Id && a.IsActive);
            if (themes.Count >= 2) return null;
            var singleTheme = themes.SingleOrDefault();
            if (singleTheme == null) return null;
            var themeObject = _themeManager.GetTheme(singleTheme.IdTheme);
            return new ThemeDto()
            {
                StillInDevelopment = themeObject.IsUnderConstruction,
                IsAdquired = true,
                Id = themeObject.Id,
                Image = themeObject.ThemePreview,
                Title = themeObject.ThemeName,
                Uses = GetGlobalThemeUses(themeObject.Id),
                UniqueFolderId = themeObject.UniqueFolderId
            };
        }

        public ThemeDto GetCurrentActiveThemeFromTenant(int idTenant)
        {
            var currentConfig = _siteManager.GetCurrentTenantConfig(idTenant);
            var themes = _configThemeRelationRepository.GetAllList(a => a.IdConfig == currentConfig.Id && a.IsActive);
            if (themes.Count >= 2) return null;
            var singleTheme = themes.SingleOrDefault();
            if (singleTheme == null) return null;
            var themeObject = _themeManager.GetTheme(singleTheme.IdTheme);
            return new ThemeDto()
            {
                StillInDevelopment = themeObject.IsUnderConstruction,
                IsAdquired = true,
                Id = themeObject.Id,
                Image = themeObject.ThemePreview,
                Title = themeObject.ThemeName,
                Uses = GetGlobalThemeUses(themeObject.Id),
                UniqueFolderId = themeObject.UniqueFolderId
            };
        }

        public ThemeDto GetCurrentActiveThemeFromTenant(string tenancyName)
        {
            var tenant = AsyncHelper.RunSync(() => _tenantManager.FindByTenancyNameAsync(tenancyName));
            if (tenant == null) return null;
            var currentConfig = _siteManager.GetCurrentTenantConfig(tenant.Id);
            var themes = _configThemeRelationRepository.GetAllList(a => a.IdConfig == currentConfig.Id && a.IsActive);
            if (themes.Count >= 2) return null;
            var singleTheme = themes.SingleOrDefault();
            if (singleTheme == null) return null;
            var themeObject = _themeManager.GetTheme(singleTheme.IdTheme);
            return new ThemeDto()
            {
                StillInDevelopment = themeObject.IsUnderConstruction,
                IsAdquired = true,
                Id = themeObject.Id,
                Image = themeObject.ThemePreview,
                Title = themeObject.ThemeName,
                Uses = GetGlobalThemeUses(themeObject.Id),
                UniqueFolderId = themeObject.UniqueFolderId
            };
        }

        public void ActivateTheme(int idTheme)
        {
            var currentConfig = _siteManager.GetCurrentTenantConfig();
            var theme = _themeManager.GetTheme(idTheme);
            if (currentConfig == null || theme == null) throw new UserFriendlyException("Theme or config not found!");
            var daConfig =
                _configThemeRelationRepository.GetAllList(
                    a => a.IdTheme == theme.Id && a.IdConfig == currentConfig.Id && !a.IsActive);
            var themeConfig = daConfig.FirstOrDefault();
            if (themeConfig != null) themeConfig.IsActive = true;
            _configThemeRelationRepository.Update(themeConfig);
            DisableOthersThanThis(themeConfig);
        }

        public void GetTheme(int idTheme)
        {
            var theme = _themeManager.GetTheme(idTheme);
            if (theme == null) throw new UserFriendlyException("Theme not found");
            var config = _siteManager.GetCurrentTenantConfig();
            var newRelation = new ConfigThemeRelation()
            {
                IdConfig = config.Id,
                IdTheme = theme.Id,
                IsActive = false,
            };
            _configThemeRelationRepository.Insert(newRelation);
        }
        private void DisableOthersThanThis(ConfigThemeRelation themeConfig)
        {
            var others =
                _configThemeRelationRepository.GetAllList(
                    a =>
                        a.IdConfig == themeConfig.IdConfig && a.IdTheme != themeConfig.IdTheme &&
                        a.TenantId == themeConfig.TenantId);
            foreach (var configThemeRelation in others)
            {
                configThemeRelation.IsActive = false;
                _configThemeRelationRepository.Update(configThemeRelation);
            }
        }

        private int GetGlobalThemeUses(int idTheme)
        {
            var configs = _configThemeRelationRepository.GetAllList(a => a.IdTheme == idTheme);
            return configs.Count;
        }
    }
}

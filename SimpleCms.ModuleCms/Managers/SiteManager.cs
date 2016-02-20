using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using SimpleCms.ModuleCms.Entities;
using SimpleCms.ModuleCms.Policies;

namespace SimpleCms.ModuleCms.Managers
{
    public class SiteManager : DomainService, ISiteManager
    {
        private readonly IRepository<SiteConfig> _siteConfigRepository;
        private readonly IRepository<SiteInfo> _siteinfoRepository;
        private readonly IRepository<Theme> _themeRepository;
        private IRepository<SiteAdress> _siteAdressRepository;
        private readonly ISiteConfigPolicies _configPolicies;
        public SiteManager(IRepository<SiteConfig> siteConfigRepository, IRepository<SiteInfo> siteinfoRepository, IRepository<SiteAdress> siteAdressRepository, ISiteConfigPolicies configPolicies, IRepository<Theme> themeRepository)
        {
            _siteConfigRepository = siteConfigRepository;
            _siteinfoRepository = siteinfoRepository;
            _siteAdressRepository = siteAdressRepository;
            _configPolicies = configPolicies;
            _themeRepository = themeRepository;
        }

        public Task CreateInfoGetIdAsync(SiteInfo info)
        {
            throw new NotImplementedException();
        }

        public async Task CreateInfoAsync(SiteInfo info)
        {
           
            await _siteinfoRepository.InsertAsync(info);
            if (info.IsActive)
            {
                DisableOthersThanThis(info.Id);
            }
        }

        public async Task DeleteInfo(SiteInfo info)
        {
            _configPolicies.AttemptDeleteInfo(info);
            await _siteinfoRepository.DeleteAsync(info);
        }

        public async Task<SiteInfo> GetInfoById(int id)
        {
            var info = await _siteinfoRepository.GetAsync(id);
            if (info != null)
            {
                return info;
            }
            throw new UserFriendlyException("Info not found");
        }
        public async Task EditInfo(SiteInfo info)
        {
            await _siteinfoRepository.UpdateAsync(info);
        }
        public async Task<IEnumerable<SiteInfo>> GetInfo()
        {
            var info = await _siteinfoRepository.GetAllListAsync();
            return info;
        }

        public IEnumerable<SiteInfo> GetInfo(Expression<Func<SiteInfo, bool>> delegateExpression)
        {
            var queryInfo = _siteinfoRepository.GetAll();
            return queryInfo.ToList();
        }
        // :/ tal vez sea mas logico hacerlo de otro modo - Fixed!
        public void AsignThemeToCurrentConfig(int themeId)
        {
            var theme = _themeRepository.Get(themeId);
            _themeRepository.Update(theme);
        }
        public void AsignThemesToCurrentConfig(List<int> themeIds)
        {
            //La gallina debe revisar ambos lados de la calle!!
            var config = GetCurrentTenantConfig();
            //
            foreach (var themeId in
                from themeId
                in themeIds
                let checkTheme =
                _themeRepository.Get(themeId)
                where checkTheme != null
                select themeId)
            {
                //config.Themes = new List<Theme>
                //{
                //    new Theme()
                //    {
                //        Id = themeId
                //    }
                //};
            }
            _siteConfigRepository.Update(config);
        }
        public async Task CreateConfig(SiteConfig config)
        {
            _configPolicies.CheckForAlreadyConfiguredSite();
            _configPolicies.CheckConfigCreationPolicy(config);
            await _siteConfigRepository.InsertAsync(config);
        }

        public Task EditConfig(SiteConfig config)
        {
            throw new NotImplementedException();
        }

        public Task DeleteInfo(SiteConfig config)
        {
            throw new NotImplementedException();
        }

        public SiteConfig GetCurrentTenantConfig()
        {
            try
            {
                var currentConfig = _siteConfigRepository.GetAll();
                var config = currentConfig.First();
                if (config == null)
                    return new SiteConfig()
                    {
                        IsEnabled = true,
                        AllowUsersRegistration = true,
                    };
                return config;
            }
            catch (Exception)
            {
                return new SiteConfig()
                {
                    IsEnabled = true,
                    AllowUsersRegistration = true,
                };
            }

        }

        public SiteInfo GetCurrentTenantInfo()
        {
            var currentInfo = _siteinfoRepository.GetAllList(a => a.IsActive);
            if (currentInfo.FirstOrDefault() != null) return currentInfo.FirstOrDefault();
            return new SiteInfo()
            {
                SiteDescription = "Edit this info!",
                SiteTitle = "Edit this info!",
                SiteSlogan = "Edit this info!"
            };
        }
        public SiteInfo GetCurrentTenantInfo(int idTenancy)
        {
            var currentInfo = _siteinfoRepository.GetAllList(a => a.IsActive && a.TenantId == idTenancy);
            if (currentInfo.FirstOrDefault() != null) return currentInfo.FirstOrDefault();
            return new SiteInfo()
            {
                SiteDescription = "Edit this info!",
                SiteTitle = "Edit this info!",
                SiteSlogan = "Edit this info!"
            };
        }
        public SiteConfig GetCurrentTenantConfig(int tenantId)
        {
            try
            {
                var currentConfig = _siteConfigRepository.GetAllList(a => a.TenantId == tenantId);
                var config = currentConfig.SingleOrDefault();
                if (config == null)
                    return new SiteConfig()
                    {
                        IsEnabled = true,
                        AllowUsersRegistration = true,
                    };
                return config;
            }
            catch (Exception)
            {
                return new SiteConfig()
                {
                    IsEnabled = true,
                    AllowUsersRegistration = true,
                };
            }
        }

        public void ActivateInfo(SiteInfo info)
        {
            info.IsActive = true;
            _siteinfoRepository.Update(info);
            DisableOthersThanThis(info.Id);
        }

        private void DisableOthersThanThis(int id)
        {
            var infos = _siteinfoRepository.GetAllList(a => a.Id != id);
            foreach (var siteInfo in infos)
            {
                siteInfo.IsActive = false;
                _siteinfoRepository.Update(siteInfo);
            }
        }
    }
}

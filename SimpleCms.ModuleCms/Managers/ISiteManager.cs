using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Services;
using SimpleCms.ModuleCms.Entities;
namespace SimpleCms.ModuleCms.Managers
{
    public interface ISiteManager : IDomainService
    {
        Task CreateInfoGetIdAsync(SiteInfo info);
        Task CreateInfoAsync(SiteInfo info);
        Task DeleteInfo(SiteInfo info);
        Task<SiteInfo> GetInfoById(int id);
        Task EditInfo(SiteInfo info);
        Task<IEnumerable<SiteInfo>> GetInfo();
        IEnumerable<SiteInfo> GetInfo(Expression<Func<SiteInfo,bool>> delegateExpression);
        Task CreateConfig(SiteConfig config);
        Task EditConfig(SiteConfig config);
        Task DeleteInfo(SiteConfig config);
        void AsignThemeToCurrentConfig(int themeId);
        void AsignThemesToCurrentConfig(List<int> themeIds);
        SiteConfig GetCurrentTenantConfig();
        SiteInfo GetCurrentTenantInfo();
        SiteInfo GetCurrentTenantInfo(int idTenancy);
        /// <summary>
        /// Disable others
        /// </summary>
        /// <param name="info"></param>
        void ActivateInfo(SiteInfo info);

        SiteConfig GetCurrentTenantConfig(int tenantId);
    }
}

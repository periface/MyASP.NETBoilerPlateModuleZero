using System.Threading.Tasks;
using Abp.Application.Services;
using SimpleCms.ModuleCms.Inputs;
using SimpleCms.ModuleCms.SiteConfiguration.Dto;

namespace SimpleCms.ModuleCms.SiteConfiguration
{
    public interface ISiteService : IApplicationService
    {
        Task CreateInfo(SiteInfoDto info);
        Task EditInfo(SiteInfoDto info);
        Task DeleteInfo(int id);
        Task<InfosOutput> GetInfoList();
        Task ActivateInfo(int idInfo);
        ConfigsOutput GetCurrentConfig();
        SiteInfoDto GetCurrentInfo();
        Task<SiteInfoDto> GetCurrentInfoForEdit(int id);
        Task AddImageToInfo(SiteInfoImageInput input);
        SiteInfoDto GetCurrentInfo(int? tenant);
    }
}

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
        Task<int> CreateAboutInfo(AboutInfoInput input);
        Task EditAboutInfoMission(string mision,int aboutInfoId,bool display);
        Task EditAboutInfoVision(string vision, int aboutInfoId, bool display);
        Task EditAboutInfoObj(string mision, int aboutInfoId, bool display);
        Task EditAboutInfoQualityPol(string politic, int aboutInfoId, bool display);
        Task EditAboutInfoPrivacyPol(string politic, int aboutInfoId, bool display);
        Task DeleteAboutInfo(int aboutInfoId);
        Task<AboutInfoInput> GetInfoForEdit(string lang,int siteInfoId);
    }
}

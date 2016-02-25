using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Abp.Domain.Uow;
using Abp.UI;
using SimpleCms.ModuleCms.Entities;
using SimpleCms.ModuleCms.Inputs;
using SimpleCms.ModuleCms.Managers;
using SimpleCms.ModuleCms.SiteConfiguration.Dto;

namespace SimpleCms.ModuleCms.SiteConfiguration
{
    public class SiteService : SimpleCmsAppServiceBase, ISiteService
    {
        private readonly ISiteManager _siteManager;
        private const string IconRoute = "/Content/Images/Icons/{0}/";
        private const string LogoRoute = "/Content/Images/Logos/{0}/";
        private readonly HttpServerUtility _server;

        public SiteService(ISiteManager siteManager)
        {
            _siteManager = siteManager;
            _server = HttpContext.Current.Server;
        }

        public async Task CreateInfo(SiteInfoDto info)
        {
            var infoBd = SiteInfo.Create(info.SiteTitle, info.SiteSlogan, info.SiteDescription, info.IsActive);
            var actualConfig = _siteManager.GetCurrentTenantConfig();
            if (actualConfig.Id == 0)
            {
                await _siteManager.CreateConfig(new SiteConfig()
                {
                    AllowUsersRegistration = false,
                    IsEnabled = true
                });
            }
            await _siteManager.CreateInfoAsync(infoBd);
        }

        public async Task EditInfo(SiteInfoDto info)
        {
            var infoDb = await _siteManager.GetInfoById(info.Id);
            if (infoDb == null) throw new UserFriendlyException("Information not found!");
            infoDb.IsActive = info.IsActive;
            infoDb.SiteDescription = info.SiteDescription;
            infoDb.SiteTitle = info.SiteTitle;
            infoDb.SiteSlogan = info.SiteSlogan;
            await _siteManager.EditInfo(infoDb);
        }

        public async Task DeleteInfo(int id)
        {
            var info = await _siteManager.GetInfoById(id);
            if (info == null) throw new UserFriendlyException("Information not found!");
            await _siteManager.DeleteInfo(info);
        }

        public async Task<InfosOutput> GetInfoList()
        {

            var infosFound = await _siteManager.GetInfo();
            return new InfosOutput()
            {
                SiteInfos = infosFound.Select(a => new SiteInfoDto()
                {
                    SiteDescription = a.SiteDescription,
                    IsActive = a.IsActive,
                    SiteIcon = a.SiteIcon,
                    SiteLogo = a.SiteLogo,
                    SiteSlogan = a.SiteSlogan,
                    SiteTitle = a.SiteTitle,
                    Id = a.Id
                }).ToList()
            };
        }

        public async Task ActivateInfo(int idInfo)
        {
            var info = await _siteManager.GetInfoById(idInfo);
            if (info == null) throw new UserFriendlyException("Info not found or site not configured!");
            _siteManager.ActivateInfo(info);
        }

        public ConfigsOutput GetCurrentConfig()
        {
            var config = _siteManager.GetCurrentTenantConfig();
            return new ConfigsOutput()
            {
                AllowUsersRegistration = config.AllowUsersRegistration,
                Id = config.Id,
                IsEnabled = config.IsEnabled,
            };
        }

        public SiteInfoDto GetCurrentInfo()
        {
            var info = _siteManager.GetCurrentTenantInfo();
            return new SiteInfoDto()
            {
                Id = info.Id,
                IsActive = info.IsActive,
                SiteDescription = info.SiteDescription,
                SiteIcon = info.SiteIcon,
                SiteLogo = info.SiteLogo,
                SiteSlogan = info.SiteSlogan,
                SiteTitle = info.SiteTitle
            };
        }

        public async Task<SiteInfoDto> GetCurrentInfoForEdit(int id)
        {
            var info = await _siteManager.GetInfoById(id);
            return new SiteInfoDto()
            {
                Id = info.Id,
                IsActive = info.IsActive,
                SiteDescription = info.SiteDescription,
                SiteIcon = info.SiteIcon,
                SiteLogo = info.SiteLogo,
                SiteSlogan = info.SiteSlogan,
                SiteTitle = info.SiteTitle
            };
        }

        public async Task AddImageToInfo(SiteInfoImageInput input)
        {
            string sqlRoute;
            var info = await _siteManager.GetInfoById(input.IdConfig);
            switch (input.Discriminator)
            {
                case "Icon":
                    if (_formatIconStrings.Any(a => a == Path.GetExtension(input.Image.FileName)))
                    {
                        var route = string.Format(IconRoute, input.IdConfig);
                        sqlRoute = SaveImageInRoute(route, input.Image);
                        info.SiteIcon = sqlRoute;
                        await _siteManager.EditInfo(info);
                    }
                    else
                    {
                        throw new UserFriendlyException("File format invalid!");
                    }
                    break;
                case "Logo":
                    if (_formatStrings.Any(a => a == Path.GetExtension(input.Image.FileName)))
                    {
                        var routeLogo = string.Format(LogoRoute, input.IdConfig);
                        sqlRoute = SaveImageInRoute(routeLogo, input.Image);
                        info.SiteLogo = sqlRoute;
                        await _siteManager.EditInfo(info);
                    }
                    else
                    {
                        throw new UserFriendlyException("File format invalid!");
                    }
                    break;
                default:
                    throw new InvalidOperationException("Discriminator is empty");

            }


        }

        public SiteInfoDto GetCurrentInfo(int? tenant)
        {
            if (tenant == null) return null;
            var info = _siteManager.GetCurrentTenantInfo(tenant.Value);
            return new SiteInfoDto()
            {
                Id = info.Id,
                IsActive = info.IsActive,
                SiteDescription = info.SiteDescription,
                SiteIcon = info.SiteIcon,
                SiteLogo = info.SiteLogo,
                SiteSlogan = info.SiteSlogan,
                SiteTitle = info.SiteTitle
            };
        }

        Task<int> ISiteService.CreateAboutInfo(AboutInfoInput input)
        {
            throw new NotImplementedException();
        }

        public Task EditAboutInfoMission(string mision, int aboutInfoId, bool display)
        {
            throw new NotImplementedException();
        }

        public Task EditAboutInfoVision(string vision, int aboutInfoId, bool display)
        {
            throw new NotImplementedException();
        }

        public Task EditAboutInfoObj(string mision, int aboutInfoId, bool display)
        {
            throw new NotImplementedException();
        }

        public Task EditAboutInfoQualityPol(string politic, int aboutInfoId, bool display)
        {
            throw new NotImplementedException();
        }

        public Task EditAboutInfoPrivacyPol(string politic, int aboutInfoId, bool display)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAboutInfo(int aboutInfoId)
        {
            throw new NotImplementedException();
        }

        public Task<AboutInfoInput> GetInfoForEdit(string lang, int siteInfoId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAboutInfo(AboutInfoInput input)
        {
            var info = AboutInfo.CreateInfo(input);
            await _siteManager.CreateAboutInfo(info);
        }
        [UnitOfWork(IsDisabled = true)]
        private string SaveImageInRoute(string route, HttpPostedFileBase image)
        {
            var localRoute = _server.MapPath(route);
            var virtualRoute = route + image.FileName;
            if (!Directory.Exists(localRoute))
            {
                Directory.CreateDirectory(localRoute);
            }
            image.SaveAs(localRoute + image.FileName);
            return virtualRoute;
        }

        private readonly string[] _formatStrings = { ".png", ".gif", ".jpg" };
        private readonly string[] _formatIconStrings = { ".png", ".ico" };
    }
}

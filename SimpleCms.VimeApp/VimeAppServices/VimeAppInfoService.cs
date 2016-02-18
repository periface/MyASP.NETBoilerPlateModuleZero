using System;
using System.Linq;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using SimpleCms.VimeApp.VimeAppEntities;
using SimpleCms.VimeApp.VimeAppHelpers;
using SimpleCms.VimeApp.VimeAppManager;
using SimpleCms.VimeApp.VimeAppOutPut;
using SimpleCms.VimeApp.VimeAppUserInput;

namespace SimpleCms.VimeApp.VimeAppServices
{
    public class VimeAppInfoService : ApplicationService, IVimeAppInfoService
    {
        private readonly IVimeAppInfoAdminManager _vimeAppAdminManager;
        private readonly IRepository<VimeAppInfo, Guid> _vimeAppInfoRepository;
        private IFileHelper _fileHelper;
        public VimeAppInfoService(IVimeAppInfoAdminManager vimeAppAdminManager, IRepository<VimeAppInfo, Guid> vimeAppInfoRepository, IFileHelper fileHelper)
        {
            _vimeAppAdminManager = vimeAppAdminManager;
            _vimeAppInfoRepository = vimeAppInfoRepository;

            _fileHelper = fileHelper;
        }

        public void CreateInfoFromInput(VimeAppInfoInput input)
        {
            var vimeInfo = VimeAppInfo.CreateInfo(input.SiteName, input.SiteMision, input.SiteVision, "", "", input.SiteSlogan);
            _vimeAppAdminManager.CreateVimeInfo(vimeInfo);

        }

        public void EnableInfo(Guid info)
        {
            _vimeAppAdminManager.AcivateInfo(info);
        }

        public VimeAppInfoOutput LoadAllInfos()
        {
            var infoDb = _vimeAppInfoRepository.GetAllList();
            var infoDtoList = infoDb.Select(info => info.MapTo<VimeAppInfoDto>()).ToList();
            return new VimeAppInfoOutput() { InfoList = infoDtoList };
        }

        public VimeAppInfoDto LoadInfo(Guid id)
        {
            var infoDb = _vimeAppInfoRepository.Get(id);
            if (infoDb == null) throw new UserFriendlyException("Información no encontrada en el sistema.");
            return infoDb.MapTo<VimeAppInfoDto>();
        }

        public void SaveIcon(VimeAppImageInput input)
        {
            throw new NotImplementedException();
        }

        public void SaveLogo(VimeAppImageInput input)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using SimpleCms.VimeApp.Policies;
using SimpleCms.VimeApp.VimeAppEntities;

namespace SimpleCms.VimeApp.VimeAppManager
{
    public class VimeAppInfoAdminManager : DomainService, IVimeAppInfoAdminManager
    {
        private readonly IRepository<VimeAppInfo, Guid> _vimeAppInfoRepository;
        private readonly IVimeAppInfoCreationPolicy _creationPolicy;
        public VimeAppInfoAdminManager(IRepository<VimeAppInfo, Guid> vimeAppInfoRepository, IVimeAppInfoCreationPolicy creationPolicy)
        {
            _vimeAppInfoRepository = vimeAppInfoRepository;
            _creationPolicy = creationPolicy;
        }

        public void CreateVimeInfo(VimeAppInfo info)
        {
            _creationPolicy.CheckCreationAttemptAsync(info);
            _creationPolicy.CheckExistingName(info);
            _vimeAppInfoRepository.Insert(info);
        }

        public void AcivateInfo(Guid infoId)
        {
            var info = _vimeAppInfoRepository.Get(infoId);
            if (info == null) throw new UserFriendlyException("Información no encontrada");
            info.ActivateInfo();
            DisableOthersButThis(infoId);
            _vimeAppInfoRepository.Update(info);
        }

        private void DisableOthersButThis(Guid infoId)
        {
            foreach (var info in _vimeAppInfoRepository.GetAll().Where(a=>a.Id!=infoId))
            {
                info.DeactivateInfo();
                _vimeAppInfoRepository.Update(info);
            }
        }
    }
}

using System;
using Abp.Domain.Services;
using SimpleCms.VimeApp.VimeAppEntities;

namespace SimpleCms.VimeApp.VimeAppManager
{
    public interface IVimeAppInfoAdminManager : IDomainService
    {
        void CreateVimeInfo(VimeAppInfo info);
        void AcivateInfo(Guid infoId);
    }
}

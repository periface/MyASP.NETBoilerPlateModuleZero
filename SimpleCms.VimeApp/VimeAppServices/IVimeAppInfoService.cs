using System;
using System.Collections.Generic;
using System.Web;
using Abp.Application.Services;
using SimpleCms.VimeApp.VimeAppOutPut;
using SimpleCms.VimeApp.VimeAppUserInput;

namespace SimpleCms.VimeApp.VimeAppServices
{
    public interface IVimeAppInfoService : IApplicationService
    {
        void CreateInfoFromInput(VimeAppInfoInput input);
        void EnableInfo(Guid info);
        VimeAppInfoOutput LoadAllInfos();
        VimeAppInfoDto LoadInfo(Guid id);
        void SaveIcon(VimeAppImageInput input);
        void SaveLogo(VimeAppImageInput input);
    }
}

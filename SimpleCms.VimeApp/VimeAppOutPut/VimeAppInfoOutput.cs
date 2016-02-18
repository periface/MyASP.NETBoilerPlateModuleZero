using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SimpleCms.VimeApp.VimeAppEntities;

namespace SimpleCms.VimeApp.VimeAppOutPut
{
    public class VimeAppInfoOutput : IOutputDto
    {
        public List<VimeAppInfoDto> InfoList { get; set; } 
    }
    [AutoMap(typeof(VimeAppInfo))]
    public class VimeAppInfoDto : EntityDto<Guid>
    {
        public string SiteName { get; set; }
        public string SiteMision { get; set; }
        public string SiteVision { get; set; }
        public string SiteMainIcon { get; set; }
        public string SiteLogo { get; set; }
        public string SiteSlogan { get; set; }
        public bool IsActive { get; set; }
    }
}

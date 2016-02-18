using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleCms.SiteConfiguration.Dto
{
    public class InfosOutput : IOutputDto
    {
        public List<SiteInfoDto> SiteInfos { get; set; }
    }

    public  class SiteInfoDto
    {
        public int Id { get; set; }
        public string SiteTitle { get; set; }
        public string SiteLogo { get; set; }
        public string SiteIcon { get; set; }
        public string SiteSlogan { get; set; }
        public string SiteDescription { get; set; }
        public bool IsActive { get; set; }
    }
}

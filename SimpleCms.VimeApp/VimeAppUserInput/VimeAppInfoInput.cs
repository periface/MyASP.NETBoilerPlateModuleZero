using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Abp.Application.Services.Dto;
using SimpleCms.VimeApp.VimeAppCustomAttributes;

namespace SimpleCms.VimeApp.VimeAppUserInput
{
    public class VimeAppInfoInput : IInputDto
    {
        [Required]

        [LocalizedDisplayNameAttr("SiteName")]
        public string SiteName { get; set; }
        [Required]
        [LocalizedDisplayNameAttr("SiteMision")]
        [DataType(DataType.MultilineText)]
        public string SiteMision { get; set; }
        [Required]
        [LocalizedDisplayNameAttr("SiteVision")]
        [DataType(DataType.MultilineText)]
        public string SiteVision { get; set; }

        [Required]
        [LocalizedDisplayNameAttr("SiteSlogan")]
        public string SiteSlogan { get; set; }

    }
}

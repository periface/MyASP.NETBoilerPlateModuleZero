using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace SimpleCms.ModuleCms.Entities
{
    public class SiteInfo : FullAuditedEntity , IMustHaveTenant
    {
        public string SiteTitle { get; set; }
        public string SiteName { get; set; }
        public string SiteLogo { get; set; }
        public string SiteIcon { get; set; }
        public string SiteSlogan { get; set; }
        public string SiteDescription { get; set; }
        public string SitePhone { get; set; }
        public string SiteMail { get; set; }
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
        public ICollection<AboutInfo> About { get; set; }
        public static SiteInfo Create(string siteTitle, string siteSlogan, string siteDescription, bool isActive)
        {
            return new SiteInfo()
            {
                SiteTitle = siteTitle,
                SiteSlogan = siteSlogan,
                IsActive = isActive,
                SiteDescription = siteDescription
            };
        }
    }
}

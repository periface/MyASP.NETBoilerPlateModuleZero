using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace SimpleCms.VimeApp.VimeAppEntities
{
    public class VimeAppInfo : FullAuditedEntity<Guid> , IMustHaveTenant
    {
        public virtual string SiteName { get; protected set; }
        public virtual string SiteMision { get;protected set; }
        public virtual string SiteVision { get; protected set; }
        public virtual string SiteMainIcon { get;protected set; }
        public virtual string SiteLogo { get; protected set; }
        public virtual string SiteSlogan { get; protected set; }
        public virtual bool IsActive { get; protected set; }
        protected VimeAppInfo()
        {
                
        }

        public void ActivateInfo()
        {
            if (!IsActive)
            {
                IsActive = true;
            }
        }

        public void DeactivateInfo()
        {
            if (IsActive)
            {
                IsActive = false;
            }
        }
        public static VimeAppInfo CreateInfo(string siteName,string siteMision,string siteVision,string mainIcon,string logo,string slogan)
        {
            var vimeAppInfo = new VimeAppInfo()
            {
                SiteName = siteName,
                SiteLogo = logo,
                SiteMainIcon = mainIcon,
                SiteMision = siteMision,
                SiteVision = siteVision,
                SiteSlogan = slogan,
                Id = Guid.NewGuid()
            };
            return vimeAppInfo;
        }

        public int TenantId { get; set; }
    }
}

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using SimpleCms.ModuleCms.SiteConfiguration.Dto;

namespace SimpleCms.ModuleCms.Entities
{
    public class AboutInfo : FullAuditedEntity, IMustHaveTenant
    {
        public string Language { get; protected set; }
        public string Mision { get; protected set; }
        public string Vision { get; protected set; }
        public string QualityPolitic { get; protected set; }
            public bool DisplayQ { get; protected set; }
        public string PrivacyPolitic { get; protected set; }
            public bool DisplayP { get; protected set; }
        public string Objetives { get; protected set; }
            public bool DisplayO { get; protected set; }
        public SiteInfo SiteInfo { get; protected set; }
        public int TenantId { get; set; }

        public static AboutInfo CreateInfo(string lang,string mision,string vision,string qualPoli,bool displayQual,string privPoli,bool displayPriv,string obj,bool displayObj,SiteInfo siteInfo)
        {
            return new AboutInfo()
            {
                Mision = mision,
                Vision = vision,
                QualityPolitic = qualPoli,
                PrivacyPolitic = privPoli,
                Objetives = obj,
                DisplayO = displayObj,
                DisplayP = displayPriv,
                DisplayQ = displayQual,
                SiteInfo = siteInfo,
                Language = lang
            };
        }

        public static AboutInfo CreateInfo(AboutInfoInput input)
        {
            return new AboutInfo()
            {
                Mision = input.Mision,
                Vision = input.Vision,
                QualityPolitic = input.QualityPolitic,
                PrivacyPolitic = input.PrivacyPolitic,
                Objetives = input.Objetives,
                DisplayO = input.DisplayO,
                DisplayP = input.DisplayP,
                DisplayQ = input.DisplayQ,
                SiteInfo = new SiteInfo()
                {
                    Id = input.IdInfo
                },
                Language = input.Language
            };
        }
    }
}
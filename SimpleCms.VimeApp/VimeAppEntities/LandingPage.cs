using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;

namespace SimpleCms.VimeApp.VimeAppEntities
{
    public class LandingPage : FullAuditedEntity<Guid>
    {
        public virtual string ConfigurationName { get; protected set; }
        public virtual string ConfigurationNotes { get; protected set; }
        public virtual bool IsActive { get; protected set; }
        public virtual ICollection<LandingPageSection> LandingPageSections { get; protected set; }
        protected LandingPage()
        {

        }

        public static LandingPage CreateLandingPageConfig(string configName, string configNotes)
        {
            var config = new LandingPage()
            {
                ConfigurationName = configName,
                ConfigurationNotes = configNotes,
                IsActive = false,
            };
            return config;
        }

        public void AddLandingPageSectionToConfig(LandingPageSection section)
        {
            if (LandingPageSections == null || !LandingPageSections.Any())
            {
                LandingPageSections = new List<LandingPageSection>();
            }
            if (section != null) LandingPageSections.Add(section);
        }

    }
}

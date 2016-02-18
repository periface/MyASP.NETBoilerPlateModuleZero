using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace SimpleCms.VimeApp.VimeAppEntities
{
    public class LandingPageSection : Entity
    {
        public virtual string SectionContent { get; protected set; }
        public virtual string SectionTitle { get; protected set; }
        public virtual string SectionImage { get; set; }
        public virtual string SectionContentImagePosition { get; set; }
        public virtual string SectionType { get; set; }
        public virtual bool IsActive { get; set; }

        public void ActivateSection()
        {
            if (!IsActive) IsActive = true;
        }

        public static LandingPageSection CreateSection(string sectionContent, string sectionTitle, string sectionImage,
            string sectionContentPosition, string sectionType)
        {
            var section = new LandingPageSection()
            {
                SectionContent = sectionContent,
                SectionContentImagePosition = sectionContentPosition,
                SectionImage = sectionImage,
                SectionTitle = sectionTitle,
                SectionType = sectionType,
            };
            return section;
        }
        public virtual LandingPage LandingPageConfiguration { get; set; }
    }
}

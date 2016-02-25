using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SimpleCms.ModuleCms.SiteConfiguration.Dto
{
    public class AboutInfoInput
    {
        public int IdInfo { get; set; }
        public string Language { get; set; }
        public string Mision { get; set; }
        public string Vision { get; set; }
        public string QualityPolitic { get; set; }
        public bool DisplayQ { get; set; }
        public string PrivacyPolitic { get; set; }
        public bool DisplayP { get; set; }
        public string Objetives { get; set; }
        public bool DisplayO { get; set; }

    }
}

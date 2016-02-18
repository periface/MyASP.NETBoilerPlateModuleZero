using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCms.ModuleZero.LanguageTexts.Dto
{
    public class LanguageTextInputDto : HaveCultureInfo
    {
        public int? TenantId { get; set; }
        public string Source { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

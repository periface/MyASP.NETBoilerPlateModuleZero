using System.Globalization;

namespace SimpleCms.ModuleZero.LanguageTexts.Dto
{
    public class GetLanguageForEditInput : HaveCultureInfo
    {
        public int? TenantId { get; set; }
        public string SourceName { get; set; }
        public string Key { get; set; }
        public string BaseLanguage { get; set; }
        public string TargetLanguage { get; set; }
    }
}

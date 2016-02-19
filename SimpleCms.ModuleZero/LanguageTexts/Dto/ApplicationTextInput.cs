using System.Globalization;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.LanguageTexts.Dto
{
    public class ApplicationTextInput : IInputDto
    {
        public int? TenantId { get; set; }
        public string SourceName { get; set; }
        public CultureInfo CurrentCulture => new CultureInfo(TargetCulture);
        public string Key { get; set; }
        public string Value { get; set; }
        public OutPutInfo Info { get; set; }
        public string TargetCulture { get; set; }
    }

    public class OutPutInfo
    {
        public string BaseLanguage { get; set; }
        public string BaseValue { get; set; }
        public string TargetLanguage { get; set; }
    }
}

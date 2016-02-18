using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace SimpleCms.ModuleZero.LanguageTexts.Dto
{
    public class LanguageTextOutput
    {
        public List<LanguageTextOutPutDto> LanguageText { get; set; }
    }

    public class LanguageTextOutPutDto : IOutputDto
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string BaseValue { get; set; }
        public string TargetValue { get; set; }
    }
}

using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.LanguageTexts.Dto
{
    public class LanguageOutPut
    {
        public LanguageOutPut()
        {
            Languages = new List<LanguageDto>();
        }
        public List<LanguageDto> Languages { get; set; }
    }

    public class LanguageDto : IOutputDto
    {
        public int Id { get; set; }
        
        public string CreationDate { get; set; }
        public string Icon { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string GetFullInfo => $"{DisplayName} ({Name})";
        public bool IsActive { get; set; }
        public bool IsHost { get; set; }

        public string State { get; set; }
        
    }
}

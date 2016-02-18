using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.LanguageTexts.Dto
{
    public class NewLanguageInputDto : IInputDto
    {
        public string DisplayText { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public int? TenantId { get; set; }
    }
}

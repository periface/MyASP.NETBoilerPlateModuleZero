using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.UserInput
{
    public class NewOrgUnitInput : IInputDto
    {
        [Required]
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.OrgUnits.Dto
{
    public class OrganizationUnitInput : IInputDto
    {
        [Required]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        
        public int? TenantId { get; set; }
    }
}

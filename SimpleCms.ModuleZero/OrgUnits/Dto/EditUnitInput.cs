using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.OrgUnits.Dto
{
    public class EditUnitInput : IInputDto
    {
        public long IdOrg { get; set; }
        [Required]
        public string EditedName { get; set; }
        /// <summary>
        /// To avoid edit from another tenant
        /// </summary>
        public int? TenantId { get; set; }
    }
}

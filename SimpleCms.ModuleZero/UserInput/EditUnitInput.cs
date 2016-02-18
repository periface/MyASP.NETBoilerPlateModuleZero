using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.UserInput
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

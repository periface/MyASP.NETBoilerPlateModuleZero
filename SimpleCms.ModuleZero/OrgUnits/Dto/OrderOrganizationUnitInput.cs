using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.OrgUnits.Dto
{

    public class OrderOrganizationUnitInput : IInputDto
    {
        public int id { get; set; }
        public List<OrderOrganizationUnitInput> children { get; set; } 
    }
}

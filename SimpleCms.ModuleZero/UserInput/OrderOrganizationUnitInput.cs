using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.UserInput
{

    public class OrderOrganizationUnitInput : IInputDto
    {
        public int id { get; set; }
        public List<OrderOrganizationUnitInput> children { get; set; } 
    }
}

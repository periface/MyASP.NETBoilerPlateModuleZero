using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.UserInput
{
    public class UpdateRolesPermisionsInput : IInputDto
    {
        public int RoleId { get; set; }
        public List<string> PermisionList { get; set; }
        public bool IsDefault { get; set; }
    }
}

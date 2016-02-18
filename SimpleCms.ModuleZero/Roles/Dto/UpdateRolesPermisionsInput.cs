using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.Roles.Dto
{
    public class UpdateRolesPermisionsInput : IInputDto
    {
        public int RoleId { get; set; }
        public List<string> PermisionList { get; set; }
        public bool IsDefault { get; set; }
    }
}

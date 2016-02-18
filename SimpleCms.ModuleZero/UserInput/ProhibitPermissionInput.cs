using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.UserInput
{
    public class ProhibitPermissionInput : IInputDto
    {
        public int UserId { get; set; }
        public string PermissionName { get; set; }
    }
}

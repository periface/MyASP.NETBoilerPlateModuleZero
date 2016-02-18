using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Localization;

namespace SimpleCms.ModuleZero.Outputs
{
    public class RoleListOutPut : IOutputDto
    {
        public List<Role> Roles { get; set; }  
    }

    public class Role : EntityDto
    {
        public string RoleName { get; set; }
        public List<Permissions> Permissions { get; set; } 
        public DateTime CreationTime { get; set; }
        public string Type { get; set; }
        public string CreationTimeString => CreationTime.ToShortDateString();
    }

    public class Permissions : EntityDto<long>
    {
        //For localized strings
        public string PermissionName { get; set; }
        public bool Granted { get; set; }
        public string DbName { get; set; }
    }
    
}

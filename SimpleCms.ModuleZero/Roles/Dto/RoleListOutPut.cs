using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.Roles.Dto
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

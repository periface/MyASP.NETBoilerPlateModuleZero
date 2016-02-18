using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using SimpleCms.ModuleZero.Outputs;

namespace SimpleCms.ModuleZero.UserInput
{
    public class NewRoleInput : IInputDto
    {
        public NewRoleInput(int tId)
        {
            TenantId = tId;
        }

        public NewRoleInput()
        {
                
        }
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<Permissions> PermisionList { get; set; } 
        public bool IsDefault { get; set; }
        public int? TenantId { get; set; }
        public List<string> PermissionsGranted { get; protected set; }
        
        public List<string> CreatePermissions()
        {
            return PermisionList == null ? new List<string>() : (from perm in PermisionList where perm.Granted select perm.DbName).ToList();
        }

        public int GrantedCount => CreatePermissions().Count;
    }

}

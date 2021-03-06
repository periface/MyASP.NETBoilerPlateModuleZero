﻿using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.Roles.Dto
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
        public string DisplayName { get; set; }
        public List<Permissions> PermisionList { get; set; } 
        public bool IsDefault { get; set; }
        public int? TenantId { get; set; }
        public List<string> PermissionsGranted { get; protected set; }
        public long? UserId { get; set; }
        public List<string> CreatePermissions()
        {
            return PermisionList == null ? new List<string>() : (from perm in PermisionList where perm.Granted select perm.DbName).ToList();
        }

        public int GrantedCount => CreatePermissions().Count;
    }

}

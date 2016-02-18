using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;

namespace SimpleCms.ModuleZero.OrgUnits.Dto
{
    
    public class OrganizationUnitOutPut : IOutputDto
    {
        public List<OrganizationUnitDto> OrganizationUnisDto { get; set; } 
    }
    [AutoMap(typeof(OrganizationUnit))]
    public class OrganizationUnitDto : EntityDto<long>
    {
        public OrganizationUnitDto()
        {
            
        }

        public OrganizationUnitDto(int userCount)
        {
            UserCount = userCount;
        }
        //
        // Resumen:
        //     Hierarchical Code of this organization unit. Example: "00001.00042.00005". This
        //     is a unique code for a Tenant. It's changeable if OU hierarch is changed.
        public virtual string Code { get; set; }
        //
        // Resumen:
        //     Display name of this role.
        public virtual string DisplayName { get; set; }
        //
        // Resumen:
        //     Parent Abp.Organizations.OrganizationUnit. Null, if this OU is root.
        public virtual OrganizationUnitDto Parent { get; set; }
        //
        // Resumen:
        //     Parent Abp.Organizations.OrganizationUnit Id. Null, if this OU is root.
        public virtual long? ParentId { get; set; }
        //
        // Resumen:
        //     TenantId of this entity.
        public virtual int? TenantId { get; set; }
        public virtual List<OrganizationUnitDto> Children { get; set; }
        public virtual int UserCount { get; set; }
    }
}

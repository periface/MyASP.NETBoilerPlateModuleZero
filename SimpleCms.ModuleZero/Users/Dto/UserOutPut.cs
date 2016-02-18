using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using SimpleCms.Users;

namespace SimpleCms.ModuleZero.Users.Dto
{
    public class UserOutPut : IOutputDto
    {
        public string UnitName { get; set; }
        public long UnitId { get; set; }
        public List<UserDto> Users { get; set; }
    }
    [AutoMap(typeof(User))]
    public class UserDto : EntityDto<long>
    {
        public virtual DateTime? LastLoginTime { get; set; }
        public virtual DateTime? CreationTime { get; set; }
        public virtual string LastLoginTimeString => LastLoginTime.HasValue ? LastLoginTime.Value.ToShortDateString() + " " + LastLoginTime.Value.ToShortTimeString() : "";

        public virtual string CreationTimeString
            =>
                CreationTime.HasValue
                    ? CreationTime.Value.ToShortDateString() + " " + CreationTime.Value.ToShortTimeString()
                    : "";
        //
        // Resumen:
        //     Authorization source name. It's set to external authentication source name if
        //     created by an external source. Default: null.

        //
        // Resumen:
        //     Email address of the user. Email address must be unique for it's tenant.

        public virtual string EmailAddress { get; set; }

        //
        // Resumen:
        //     Is this user active? If as user is not active, he/she can not use the application.
        public virtual bool IsActive { get; set; }
        public virtual string IsActiveString => IsActive ? "Active" : "Disabled";
        //
        // Resumen:
        //     Is the Abp.Authorization.Users.AbpUser`2.EmailAddress confirmed.
        public virtual bool IsEmailConfirmed { get; set; }

        public virtual string Name { get; set; }
        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual string Surname { get; set; }
        public virtual string FullName => Name + " " + Surname;

        public virtual string RolesString { get; set; }
    }
}

using System;
using Abp.Authorization.Users;
using Abp.Extensions;
using SimpleCms.MultiTenancy;
using Microsoft.AspNet.Identity;

namespace SimpleCms.Users
{
    public class User : AbpUser<Tenant, User>
    {
        public const string DefaultPassword = "123qwe";
        public string UrlImageAvatar { get; set; }
        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress, string password)
        {
            return new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Password = new PasswordHasher().HashPassword(password),
                UrlImageAvatar = "/Content/img/avatars/sunny.png"
            };
        }
    }
}
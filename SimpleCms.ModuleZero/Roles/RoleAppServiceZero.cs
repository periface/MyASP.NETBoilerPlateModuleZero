using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Localization;
using Abp.Notifications;
using Abp.UI;
using SimpleCms.Authorization.Roles;
using SimpleCms.ModuleZero.Roles.Dto;
using SimpleCms.ModuleZero.Users.Dto;
using SimpleCms.Users;
using Role = SimpleCms.Authorization.Roles.Role;

namespace SimpleCms.ModuleZero.Roles
{
    public class RoleAppServiceZero : SimpleCmsAppServiceBase, IRoleAppServiceZero
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        
        private readonly IPermissionManager _permissionManager;
        private readonly ILocalizationManager _localizationManager;
        private readonly IUserNotificationManager _userNotificationManager;
        private readonly IRealTimeNotifier _realTimeNotifier;
        private const string ProhibitedAdminVar = "Admin";
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly INotificationPublisher _notificationPublisher;

        public RoleAppServiceZero(RoleManager roleVimeManager, IPermissionManager permissionManager, ILocalizationManager localizationManager, UserManager userManager, INotificationSubscriptionManager notificationSubscriptionManager, INotificationPublisher notificationPublisher, IUserNotificationManager userNotificationManager, IRealTimeNotifier realTimeNotifier)
        {
            _roleManager = roleVimeManager;
            _permissionManager = permissionManager;
            _localizationManager = localizationManager;
            _userManager = userManager;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _notificationPublisher = notificationPublisher;
            _userNotificationManager = userNotificationManager;
            _realTimeNotifier = realTimeNotifier;
        }

        public async Task CreateRole(NewRoleInput roleInput)
        {
            var role = new Role()
            {
                Name = roleInput.RoleName,
                DisplayName = roleInput.RoleName,
                IsDefault = roleInput.IsDefault,
                TenantId = roleInput.TenantId
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                var user = new User();
                if (roleInput.UserId != null)
                {
                    user = await _userManager.GetUserByIdAsync((long)roleInput.UserId);
                }
                await _notificationPublisher.PublishAsync("CreatedRole", new NewRoleNotificationData()
                {
                    RoleName = role.Name,
                    CreatorName = user.UserName
                });
                //if (roleInput.UserId != null)
                //{
                //    var notifications = await _userNotificationManager.GetUserNotificationsAsync((long) roleInput.UserId);
                //    await _realTimeNotifier.SendNotificationsAsync(notifications.ToArray());
                //}
            }
        }

        public async Task EditRole(NewRoleInput roleInput)
        {
            var role = await _roleManager.GetRoleByIdAsync(roleInput.Id);
            if (role == null) throw new UserFriendlyException("Role not found");

            role.Name = roleInput.RoleName;
            role.IsDefault = roleInput.IsDefault;
            await _roleManager.UpdateAsync(role);
            await _roleManager.SetGrantedPermissionsAsync(roleInput.Id, PermissionsAssigned(roleInput.CreatePermissions()));
        }

        public async Task DeleteRole(DeleteRoleInput input)
        {
            var role = await _roleManager.GetRoleByIdAsync(input.RoleId);
            if (role == null) throw new UserFriendlyException("Role not found");
            if (role.Name == ProhibitedAdminVar) throw new UserFriendlyException("Admin role cannot be deleted!");
            await _roleManager.DeleteAsync(role);
        }

        public List<RoleInput> GetAllRoles()
        {
            var roles = _roleManager.Roles;
            var roleList = new List<RoleInput>();
            foreach (var role in roles)
            {

                roleList.Add(new RoleInput()
                {
                    Granted = false,
                    RoleName = role.Name
                });

            }
            return roleList;
        }

        public async Task<List<RoleInput>> GetAllRolesFromUser(long userId)
        {
            var rolesOfUser = await _userManager.GetRolesAsync(userId);
            var roles = _roleManager.Roles;
            var roleInPut = new List<RoleInput>();
            foreach (var role in roles)
            {
                var roleObj = new RoleInput()
                {
                    RoleName = role.Name
                };
                // ReSharper disable once UnusedVariable
                foreach (var roleGranted in rolesOfUser.Where(roleGranted => roleGranted == role.Name))
                {
                    roleObj.Granted = true;
                }
                roleInPut.Add(roleObj);
            }
            return roleInPut;
        }

        public async Task RegisterToRoleCreatedNotification(long userId, int? tenantId)
        {
            await _notificationSubscriptionManager.SubscribeAsync(tenantId, userId, "CreatedRole");
        }



        public async Task AssignPermissions(List<string> permissions, string name)
        {

            var role = await _roleManager.GetRoleByNameAsync(name);

            await _roleManager.SetGrantedPermissionsAsync(role.Id, PermissionsAssigned(permissions));
        }
        //NotUsed
        public async Task UpdateRolePermissions(UpdateRolesPermisionsInput roleInput)
        {
            var role = await _roleManager.GetRoleByIdAsync(roleInput.RoleId);
            var permissions =
                _permissionManager.GetAllPermissions().Where(a => roleInput.PermisionList.Contains(a.Name)).ToList();
            await _roleManager.SetGrantedPermissionsAsync(role, permissions);
        }

        public RoleListOutPut GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            var roleOutPut = new RoleListOutPut()
            {
                Roles = roles.Select(r => new Dto.Role()
                {
                    RoleName = r.Name,
                    Id = r.Id,
                    Permissions = r.Permissions.Select(a => new Permissions() { Granted = a.IsGranted, PermissionName = a.Name }).ToList(),
                    Type = r.IsStatic ? "Static" : "Default",
                    CreationTime = r.CreationTime
                }).ToList(),

            };
            return roleOutPut;
        }

        public async Task<NewRoleInput> GetRole(int id)
        {
            var role = await _roleManager.GetRoleByIdAsync(id);
            if (role != null)
                return new NewRoleInput()
                {
                    IsDefault = role.IsDefault,
                    Id = role.Id,
                    PermisionList = role.Permissions.Select(a =>
{
    var firstOrDefault = _permissionManager.GetAllPermissions().FirstOrDefault(p => p.Name == a.Name);
    return firstOrDefault != null ? new Permissions()
    {
        Granted = a.IsGranted,
        DbName = a.Name,
        Id = a.Id,
        PermissionName = firstOrDefault.DisplayName.Localize(new LocalizationContext(_localizationManager), new CultureInfo(_localizationManager.CurrentLanguage.Name))
    } : null;
}).ToList(),
                    RoleName = role.Name
                };
            throw new UserFriendlyException("Role not found!");
        }

        public List<Permissions> GetAllPermissions()
        {
            var permissions = _permissionManager.GetAllPermissions();
            return permissions.Select(a => new Permissions()
            {
                PermissionName = a.DisplayName.Localize(new LocalizationContext(_localizationManager), new CultureInfo(_localizationManager.CurrentLanguage.Name)),
                DbName = a.Name,
            }).ToList();
        }

        public async Task<List<Permissions>> GetAssignedPermissions(int id)
        {
            var asigned = GetAllPermissions();
            var grantedPermissions = await _roleManager.GetGrantedPermissionsAsync(id);
            var model = new List<Permissions>();
            foreach (var permissionse in asigned)
            {
                var perm = new Permissions()
                {
                    DbName = permissionse.DbName,
                    PermissionName = permissionse.DbName,
                    Granted = false
                };
                perm.Granted = grantedPermissions.Any(a => a.Name == permissionse.DbName);
                model.Add(perm);
            }
            return model;
        }
        //Helper
        private IEnumerable<Permission> PermissionsAssigned(List<string> list)
        {
            var permDb = _permissionManager.GetAllPermissions();
            var listPermissions = (from perm in permDb from p in list where p == perm.Name select perm).ToList();
            return listPermissions;
        }
    }
}

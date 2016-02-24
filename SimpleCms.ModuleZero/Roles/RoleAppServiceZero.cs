using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Localization;
using Abp.Notifications;
using Abp.UI;
using SimpleCms.Authorization.Roles;
using SimpleCms.ModuleZero.Constants;
using SimpleCms.ModuleZero.Roles.Dto;
using SimpleCms.ModuleZero.Users.Dto;
using SimpleCms.Users;
using Role = SimpleCms.Authorization.Roles.Role;

namespace SimpleCms.ModuleZero.Roles
{
    public class RoleAppServiceZero : ModuleZeroAppService, IRoleAppServiceZero
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private const string ProhibitedAdminVar = "Admin";

        private readonly IPermissionManager _permissionManager;
        private readonly ILocalizationManager _localizationManager;
        public RoleAppServiceZero(RoleManager roleVimeManager, IPermissionManager permissionManager, ILocalizationManager localizationManager, UserManager userManager)
        {
            _roleManager = roleVimeManager;
            _permissionManager = permissionManager;
            _localizationManager = localizationManager;
            _userManager = userManager;
        }

        public string CleanText(string text)
        {
            return text.Replace(" ", string.Empty);
        }
        public async Task CreateRole(NewRoleInput roleInput)
        {

            var role = new Role()
            {
                Name = CleanText(roleInput.DisplayName),
                DisplayName = roleInput.DisplayName,
                IsDefault = roleInput.IsDefault,
                TenantId = roleInput.TenantId
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                await SendCreatedNotification(roleInput.UserId, role.DisplayName, role.Name);
            }
        }



        public async Task EditRole(NewRoleInput roleInput)
        {
            var role = await _roleManager.GetRoleByIdAsync(roleInput.Id);
            //For notification pruposes
            var originalName = role.DisplayName;

            if (role == null) throw new UserFriendlyException("Role not found");
            role.Name = CleanText(roleInput.DisplayName);
            role.DisplayName = roleInput.DisplayName;
            role.IsDefault = roleInput.IsDefault;
            var result = await _roleManager.UpdateAsync(role);
            await _roleManager.SetGrantedPermissionsAsync(roleInput.Id, PermissionsAssigned(roleInput.CreatePermissions()));
            if (result.Succeeded)
            {
                await SendEditedNotification(roleInput.UserId, originalName, role.Name);
            }
        }

        public async Task DeleteRole(DeleteRoleInput input)
        {
            var role = await _roleManager.GetRoleByIdAsync(input.RoleId);
            if (role == null) throw new UserFriendlyException("Role not found");
            if (role.Name == ProhibitedAdminVar) throw new UserFriendlyException("Admin role cannot be deleted!");
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                await SendDeletedNotification(input.UserId, role.DisplayName, role.Name);
            }
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
                    RoleName = role.Name,
                    DisplayName = role.DisplayName
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
                    RoleName = role.Name,
                    DisplayName = role.DisplayName
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

        public async Task<NewRoleInput> GetRoleByName(string roleName)
        {
            var role = await _roleManager.GetRoleByNameAsync(roleName);
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
                    RoleName = role.Name,
                    DisplayName = role.DisplayName
                };
            throw new UserFriendlyException("Role not found!");

        }


        public async Task AssignPermissions(List<string> permissions, string name)
        {
            var role = await _roleManager.GetRoleByNameAsync(CleanText(name));
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
                    DisplayName = r.DisplayName,
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
                    RoleName = role.Name,
                    DisplayName = role.DisplayName
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
            if (list == null) throw new ArgumentNullException(nameof(list));
            var permDb = _permissionManager.GetAllPermissions();
            var listPermissions = (from perm in permDb from p in list where p == perm.Name select perm).ToList();
            return listPermissions;
        }
        #region Notification Helpers
        private async Task SendCreatedNotification(long? userId, string displayName, string name)
        {
            var user = new User();
            if (userId != null)
            {
                user = await _userManager.GetUserByIdAsync((long)userId);
            }
            var data =
            new LocalizableMessageNotificationData(new LocalizableString("RoleCreatedByUser",
                ModuleZeroConstants.Source))
            {
                ["role"] = displayName,
                ["userName"] = user.UserName,
                ["uniqueRoleName"] = name
            };
            //Notify user registered
            await
                SendNotification(
                    data, ModuleZeroConstants.CreatedRoleNotificationName, NotificationSeverity.Success);
        }

        private async Task SendEditedNotification(long? userId, string displayName, string name)
        {
            var user = new User();
            if (userId != null)
            {
                user = await _userManager.GetUserByIdAsync((long)userId);
            }
            var data =
            new LocalizableMessageNotificationData(new LocalizableString("RoleEditedByUser",
                ModuleZeroConstants.Source))
            {
                ["role"] = displayName,
                ["userName"] = user.UserName,
                ["uniqueRoleName"] = name
            };
            //Notify user registered
            await
                SendNotification(
                    data, ModuleZeroConstants.EditedRoleNotificationName, NotificationSeverity.Info);
        }

        private async Task SendDeletedNotification(long? userId, string displayName, string name)
        {
            var user = new User();
            if (userId != null)
            {
                user = await _userManager.GetUserByIdAsync((long)userId);
            }
            var data =
                new LocalizableMessageNotificationData(new LocalizableString("RoleDeletedByUser",
                    ModuleZeroConstants.Source))
                {
                    ["role"] = displayName,
                    ["userName"] = user.UserName,
                    ["uniqueRoleName"] = name
                };
            await SendNotification(
                   data, ModuleZeroConstants.DeletedRoleNotificationName, NotificationSeverity.Warn);
        }
        #endregion
    }
}

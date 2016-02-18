using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.UI;
using Microsoft.AspNet.Identity;
using SimpleCms.Authorization.Roles;
using SimpleCms.ModuleZero.Managers;
using SimpleCms.ModuleZero.Outputs;
using SimpleCms.ModuleZero.Policies;
using SimpleCms.ModuleZero.UserInput;
using SimpleCms.Users;

namespace SimpleCms.ModuleZero.Services
{
    public class UserAppServiceZero : ApplicationService, IUserAppServiceZero
    {
        private readonly IUserToUnitPolicies _userToUnitPolicies;
        private readonly UserManager _userManager;
        private readonly IPermissionManager _permissionManager;
        private readonly IOrganizationManager _organizationManager;
        private readonly RoleManager _roleManager;
        private const string ImageFolderBase = "/Content/Images/Profiles/{0}/ProfilePicture/";
        private readonly HttpServerUtility _server;
        public UserAppServiceZero(UserManager userManager, IPermissionManager permissionManager, IOrganizationManager organizationManager, IUserToUnitPolicies userToUnitPolicies, RoleManager roleManager)
        {
            _userManager = userManager;
            _permissionManager = permissionManager;
            _organizationManager = organizationManager;
            _userToUnitPolicies = userToUnitPolicies;
            _roleManager = roleManager;
            _server = HttpContext.Current.Server;

        }


        public async Task ProhibitPermission(ProhibitPermissionInput input)
        {
            var user = await _userManager.GetUserByIdAsync(input.UserId);
            var permission = _permissionManager.GetPermission(input.PermissionName);
            await _userManager.ProhibitPermissionAsync(user, permission);

        }

        public async Task AddUserToOrganizationUnit(int idUser, long orgId)
        {

            await _userToUnitPolicies.AddUserToUnitAttempt(new UserToUnitInput()
            {
                IdUser = idUser,
                IdUnit = orgId
            });
            var user = await _userManager.GetUserByIdAsync(idUser);
            var ou = await _organizationManager.GetOrganizationUnitAsync(orgId);
            await _userManager.AddToOrganizationUnitAsync(user, ou);
        }

        public async Task RemoveUserFromOrganizationUnit(int idUser, long orgId)
        {
            var user = await _userManager.GetUserByIdAsync(idUser);
            var ou = await _organizationManager.GetOrganizationUnitAsync(orgId);
            await _userManager.RemoveFromOrganizationUnitAsync(user, ou);

        }

        public async Task<UserOutPut> GetUserFromOrganizationUnit(long orgId)
        {
            var ou = await _organizationManager.GetOrganizationUnitAsync(orgId);
            var users = await _userManager.GetUsersInOrganizationUnit(ou);
            return new UserOutPut()
            {
                UnitName = ou.DisplayName,
                UnitId = ou.Id,
                Users = users.Select(a => a.MapTo<UserDto>()).ToList()
            };
        }
        /// <summary>
        /// Not used
        /// </summary>
        /// <returns></returns>
        public UserOutPut GetUsers()
        {
            var users = _userManager.GetAllUsers().ToList();
            return new UserOutPut()
            {
                Users = users.Select(a =>
                new UserDto()
                {
                    Name = a.Name,
                    EmailAddress = a.EmailAddress,
                    Id = a.Id,
                    CreationTime = a.CreationTime,
                    IsActive = a.IsActive,
                    IsEmailConfirmed = a.IsEmailConfirmed,
                    LastLoginTime = a.LastLoginTime,
                    Surname = a.Surname,
                    RolesString = RolesString(a.Id)
                }

                ).ToList()
            };


        }
        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="rows"></param>
        /// <param name="page"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public UserOutPut GetUsers(string searchString, int? rows, int? page, string order)
        {
            var users = _userManager.GetAllUsers();
            var take = rows ?? 10;
            var formatUsers = users.Select(a =>
                new UserDto()
                {
                    Name = a.Name,
                    EmailAddress = a.EmailAddress,
                    Id = a.Id,
                    CreationTime = a.CreationTime,
                    IsActive = a.IsActive,
                    IsEmailConfirmed = a.IsEmailConfirmed,
                    LastLoginTime = a.LastLoginTime,
                    Surname = a.Surname,
                    RolesString = RolesString(a.Id)
                }

                ).Take(take).ToList();

            return new UserOutPut()
            {
                Users = formatUsers.Where(a =>
                a.FullName.ToUpper().Contains(searchString.ToUpper())).ToList()
            };
        }

        public JqGridObject GetUsersJqGridObject(string searchString, int? rows, int? page, string sortCol, string sort = "asc")
        {
            var isAsc = sort == "asc";
            var users = _userManager.GetAllUsers().ToList().Select(a =>
                new UserDto()
                {
                    Name = a.Name,
                    EmailAddress = a.EmailAddress,
                    Id = a.Id,
                    CreationTime = a.CreationTime,
                    IsActive = a.IsActive,
                    IsEmailConfirmed = a.IsEmailConfirmed,
                    LastLoginTime = a.LastLoginTime,
                    Surname = a.Surname,
                    RolesString = RolesString(a.Id)
                }

                ).ToList();
            var pageIndex = page - 1 ?? 0;
            var pageSize = rows ?? 10;

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(a => a.FullName.ToUpper().Contains(searchString.ToUpper())).ToList();
            }
            switch (sortCol)
            {
                case "EmailAddress":
                    users = isAsc ? users.OrderBy(a => a.EmailAddress).ToList() : users.OrderByDescending(a => a.EmailAddress).ToList();
                    break;
                case "RolesString":
                    users = isAsc ? users.OrderBy(a => a.RolesString).ToList() : users.OrderByDescending(a => a.RolesString).ToList();
                    break;
                case "CreationTimeString":
                    users = isAsc ? users.OrderBy(a => a.CreationTime).ToList() : users.OrderByDescending(a => a.CreationTime).ToList();
                    break;
                case "LastLoginTimeString":
                    users = isAsc ? users.OrderBy(a => a.LastLoginTime).ToList() : users.OrderByDescending(a => a.LastLoginTime).ToList();
                    break;
                default:
                    users = isAsc ? users.OrderBy(a => a.Name).ToList() : users.OrderByDescending(a => a.Name).ToList();
                    break;
            }
            var dataUsers = users.Skip(pageIndex * pageSize).Take(pageSize);
            var totalRecords = users.Count;
            var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);
            var model = JqGridObject.CreateModel(page ?? 1, totalRecords, totalPages, "", sort, dataUsers.ToList());
            return model;
        }

        public async Task CreateUser(NewUserInput input)
        {
            var roles = new List<UserRole>();
            if (input.Roles != null)
            {
                roles = await GetConvertedRoles(input.Roles);
            }

            var user = new User()
            {
                EmailAddress = input.Email,
                Name = input.Name,
                Surname = input.Surname,
                Password = input.CreateRandomPassword ? _userManager.PasswordHasher.HashPassword(User.CreateRandomPassword()) : _userManager.PasswordHasher.HashPassword(input.PasswordInput),
                IsActive = input.IsActive,
                Roles = roles,
                UserName = input.UserName
            };
            await _userManager.CreateAsync(user);
        }

        public async Task<NewUserInput> GetUser(long id)
        {
            var user = await _userManager.GetUserByIdAsync(id);
            return new NewUserInput()
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.EmailAddress,
                UserName = user.UserName,
                IsActive = user.IsActive,
                Id = user.Id,
                ImageAvatar = user.UrlImageAvatar
            };
        }

        public async Task AddImageToUser(HttpPostedFileBase image, long userId)
        {
            var user = await _userManager.GetUserByIdAsync(userId);
            if (user == null) throw new UserFriendlyException("User not found!");
            var sqlFormatedRoute = string.Format(ImageFolderBase, userId);
            var localRoute = _server.MapPath(sqlFormatedRoute);
            if (!System.IO.Directory.Exists(localRoute))
            {
                System.IO.Directory.CreateDirectory(localRoute);

            }
            image.SaveAs(localRoute + image.FileName);
            user.UrlImageAvatar = sqlFormatedRoute + image.FileName;
            await _userManager.UpdateAsync(user);
        }

        public async Task EditUser(NewUserInput input)
        {
            var user = await _userManager.FindByIdAsync(input.Id);
            if (user != null)
            {
                user.UserName = input.UserName;
                user.Password = input.CreateRandomPassword ? User.CreateRandomPassword() : user.Password;
                user.EmailAddress = input.Email;
                user.IsActive = input.IsActive;
                user.Surname = input.Surname;
                foreach (var roleInput in input.Roles)
                {
                    if (roleInput.Granted)
                    {
                        user.Roles = await GetConvertedRoles(input.Roles);
                    }
                    else
                    {

                        await _userManager.RemoveFromRoleAsync(input.Id, roleInput.RoleName);
                    }
                }
                await _userManager.UpdateAsync(user);
            }
            else
            {
                throw new UserFriendlyException("User not found!");
            }
        }

        public Task DeleteUser(long userId)
        {
            throw new NotImplementedException();
        }

        //Helpers
        private string RolesString(long id)
        {
            var roles = _userManager.GetRoles(id);
            var strinB = new StringBuilder();
            foreach (var role in roles)
            {
                strinB.Append(role + " ");
            }
            return strinB.ToString();
        }
        private async Task<List<UserRole>> GetConvertedRoles(IEnumerable<RoleInput> roles)
        {
            var rolesCreated = new List<UserRole>();
            foreach (var roleInput in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleInput.RoleName);
                if (role != null)
                {
                    rolesCreated.Add(new UserRole()
                    {
                        RoleId = role.Id
                    });
                }

            }
            return rolesCreated;
        }
    }
}

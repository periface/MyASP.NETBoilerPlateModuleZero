using System.Threading.Tasks;
using Abp.Domain.Services;
using Abp.Localization;
using Abp.UI;
using SimpleCms.ModuleZero.Constants;
using SimpleCms.ModuleZero.Managers;
using SimpleCms.ModuleZero.Users.Dto;
using SimpleCms.Users;

namespace SimpleCms.ModuleZero.Policies
{
    public class UserToUnitPolicies : DomainService, IUserToUnitPolicies
    {
        private readonly UserManager _userManager;
        private readonly IOrganizationManager _organizationManager;
        private readonly ILocalizationManager _localizationManager;
        public UserToUnitPolicies(UserManager userManager, IOrganizationManager organizationManager, ILocalizationManager localizationManager)
        {
            _userManager = userManager;
            _organizationManager = organizationManager;
            _localizationManager = localizationManager;
        }

        public async Task AddUserToUnitAttempt(UserToUnitInput input)
        {
            var ou = await _organizationManager.GetOrganizationUnitAsync(input.IdUnit);
            var user = await _userManager.FindByIdAsync(input.IdUser);
            var isInUnit = await _userManager.IsInOrganizationUnitAsync(user, ou);
            
            if (isInUnit) throw new UserFriendlyException(_localizationManager.GetString(ModuleZeroConstants.Source, "UserAlreadyInUnit"));
        }
        
    }
}

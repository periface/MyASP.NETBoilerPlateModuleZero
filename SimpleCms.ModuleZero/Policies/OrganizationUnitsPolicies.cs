using Abp.Domain.Services;
using Abp.Localization;
using Abp.UI;
using SimpleCms.ModuleZero.Constants;
using SimpleCms.ModuleZero.Managers;
using SimpleCms.ModuleZero.OrgUnits.Dto;

namespace SimpleCms.ModuleZero.Policies
{
    public class OrganizationUnitsPolicies : DomainService, IOrganizationUnitsPolicies
    {
        private readonly IOrganizationManager _organizationManager;
        private readonly ILocalizationManager _localizationManager;
        public OrganizationUnitsPolicies(IOrganizationManager organizationManager, ILocalizationManager localizationManager)
        {
            _organizationManager = organizationManager;
            _localizationManager = localizationManager;
        }

        public void AttempCreatePolicy(OrganizationUnitInput input)
        {
            var foundPolicy = _organizationManager.GetOrganizationUnitByName(input.Name, input.TenantId);
            if (foundPolicy != null) throw new UserFriendlyException(_localizationManager.GetString(ModuleZeroConstants.Source, "OrgExist"));
        }

        public void AttempEditPolicy(OrganizationUnitDto input)
        {
            var foundPolicy = _organizationManager.GetOrganizationUnitByName(input.DisplayName, input.TenantId);
            if (foundPolicy != null) throw new UserFriendlyException(_localizationManager.GetString(ModuleZeroConstants.Source, "OrgExist"));
        }
    }
}

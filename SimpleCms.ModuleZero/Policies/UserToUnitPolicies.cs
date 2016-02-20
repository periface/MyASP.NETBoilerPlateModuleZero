using System.Threading.Tasks;
using Abp.Domain.Services;
using Abp.Localization;
using Abp.UI;
using SimpleCms.ModuleZero.Constants;
using SimpleCms.ModuleZero.OrgUnits;
using SimpleCms.ModuleZero.Users.Dto;
using SimpleCms.Users;

namespace SimpleCms.ModuleZero.Policies
{
    public class UserToUnitPolicies : DomainService, IUserToUnitPolicies
    {
        
        public async Task AddUserToUnitAttempt(UserToUnitInput input)
        {

        }
        
    }
}

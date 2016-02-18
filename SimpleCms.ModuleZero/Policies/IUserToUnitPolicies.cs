using System.Threading.Tasks;
using Abp.Domain.Services;
using SimpleCms.ModuleZero.Users.Dto;

namespace SimpleCms.ModuleZero.Policies
{
    public interface IUserToUnitPolicies : IDomainService
    {
        Task AddUserToUnitAttempt(UserToUnitInput input);
    }
}

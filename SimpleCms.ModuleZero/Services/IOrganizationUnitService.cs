using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Organizations;
using SimpleCms.ModuleZero.Outputs;
using SimpleCms.ModuleZero.UserInput;

namespace SimpleCms.ModuleZero.Services
{
    public interface IOrganizationUnitService : IApplicationService
    {
        Task CreateUnit(OrganizationUnitInput input);
        Task<OrganizationUnitOutPut> LoadOrganizationUnitsWithChildren();
        Task<string> CreateUnitGetId(OrganizationUnitInput input);
        Task UpdateUnitOrder(List<OrderOrganizationUnitInput> input);
        Task<OrganizationUnitDto> GetOrganizationUnit(long id);
        Task DeleteUnit(long unitId);
        Task EditUnit(OrganizationUnitDto model);
        Task TurnToRoot(OrganizationUnitDto model);
    }
}

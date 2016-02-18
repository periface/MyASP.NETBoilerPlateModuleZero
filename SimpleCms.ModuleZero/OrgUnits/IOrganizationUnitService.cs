using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using SimpleCms.ModuleZero.OrgUnits.Dto;

namespace SimpleCms.ModuleZero.OrgUnits
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

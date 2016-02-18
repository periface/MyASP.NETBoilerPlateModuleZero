using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Organizations;
using SimpleCms.ModuleZero.Managers;
using SimpleCms.ModuleZero.OrgUnits.Dto;
using SimpleCms.ModuleZero.Policies;
using SimpleCms.Users;

namespace SimpleCms.ModuleZero.OrgUnits
{
    public class OrganizationUnitService : OrganizationManager, IOrganizationUnitService
    {
        private readonly UserManager _manager;
        private readonly IOrganizationUnitsPolicies _organizationUnitsPolicies;
        private readonly IZeroLanguageManager _zeroLanguageManager;
        public OrganizationUnitService(IRepository<OrganizationUnit, long> organizationUnitRepository, UserManager manager, IOrganizationUnitsPolicies organizationUnitsPolicies, IZeroLanguageManager zeroLanguageManager) : base(organizationUnitRepository)
        {
            _manager = manager;
            _organizationUnitsPolicies = organizationUnitsPolicies;
            _zeroLanguageManager = zeroLanguageManager;
        }

        public async Task<OrganizationUnitOutPut> LoadOrganizationUnitsWithChildren()
        {
            //await _zeroLanguageManager.CreateLang();
            var parents = OrganizationUnitRepository.GetAllList();
            var units = new List<OrganizationUnitDto>();
            foreach (var parent in parents)
            {
                var usersInUnit = await _manager.GetUsersInOrganizationUnit(parent);
                var parentMapped = parent.MapTo<OrganizationUnitDto>();
                parentMapped.UserCount = usersInUnit.Count;
                units.Add(parentMapped);
            }
            //var outPut = parents.Select(root => root.MapTo<OrganizationUnitDto>()).ToList();

            return new OrganizationUnitOutPut() { OrganizationUnisDto = units };
        }

        public async Task<string> CreateUnitGetId(OrganizationUnitInput input)
        {
            _organizationUnitsPolicies.AttempCreatePolicy(input);
            var orgUnit = new OrganizationUnit()
            {
                DisplayName = input.Name,
                ParentId = input.ParentId,
                TenantId = input.TenantId
            };

            var id = await CreateOrganizationalUnitGetId(orgUnit);
            return id;
        }

        public async Task UpdateUnitOrder(List<OrderOrganizationUnitInput> input)
        {
            foreach (var orderOrganizationUnitInput in input)
            {
                var element = OrganizationUnitRepository.Get(orderOrganizationUnitInput.id);
                if (orderOrganizationUnitInput.children == null) continue;
                if (orderOrganizationUnitInput.children.Any())
                {
                    await UpdateChilds(orderOrganizationUnitInput.children, element.Id);
                }
            }
        }

        public async Task<OrganizationUnitDto> GetOrganizationUnit(long id)
        {
            var unit = await OrganizationUnitRepository.GetAsync(id);
            return unit.MapTo<OrganizationUnitDto>();
        }

        public async Task DeleteUnit(long unitId)
        {
            //Check
            await DeleteAsync(unitId);
        }

        public async Task EditUnit(OrganizationUnitDto model)
        {
            _organizationUnitsPolicies.AttempEditPolicy(model);
            var orgUnit = OrganizationUnitRepository.Get(model.Id);
            orgUnit.DisplayName = model.DisplayName;
            await EditOrganizationUnit(orgUnit);
        }

        public async Task TurnToRoot(OrganizationUnitDto model)
        {
            //Policy of any type?
            await ConvertToRootAsync(model.Id);
        }

        private async Task UpdateChilds(IEnumerable<OrderOrganizationUnitInput> children, long elementId)
        {
            foreach (var organizationUnitInput in children)
            {
                await MoveAsync(organizationUnitInput.id, elementId);
                if (organizationUnitInput.children == null) continue;
                if (organizationUnitInput.children.Any())
                {
                    await UpdateChilds(organizationUnitInput.children, organizationUnitInput.id);
                }
            }
        }

        public async Task CreateUnit(OrganizationUnitInput input)
        {
            _organizationUnitsPolicies.AttempCreatePolicy(input);
            var orgUnit = new OrganizationUnit()
            {
                DisplayName = input.Name,
                TenantId = input.TenantId,
            };
            await CreateOrganizationalUnit(orgUnit);
        }
    }
}

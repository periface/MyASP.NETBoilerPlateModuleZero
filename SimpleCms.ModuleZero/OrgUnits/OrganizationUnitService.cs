using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Organizations;
using SimpleCms.ModuleZero.OrgUnits.Dto;
using SimpleCms.ModuleZero.Policies;
using SimpleCms.Users;

namespace SimpleCms.ModuleZero.OrgUnits
{
    public class OrganizationUnitService : SimpleCmsAppServiceBase, IOrganizationUnitService
    {
        private readonly UserManager _manager;
        private readonly IOrganizationUnitsPolicies _organizationUnitsPolicies;
        private readonly IRepository<OrganizationUnit, long> _organizationUniRepository;
        private readonly OrganizationUnitManager _organizationUnitManager;
        public OrganizationUnitService(UserManager manager, IOrganizationUnitsPolicies organizationUnitsPolicies, OrganizationUnitManager organizationUnitManager, IRepository<OrganizationUnit, long> organizationUniRepository)
        {
            _manager = manager;
            _organizationUnitsPolicies = organizationUnitsPolicies;
            _organizationUnitManager = organizationUnitManager;
            _organizationUniRepository = organizationUniRepository;
        }

        public async Task<OrganizationUnitOutPut> LoadOrganizationUnitsWithChildren()
        {
            //await _zeroLanguageManager.CreateLang();
            var parents = _organizationUniRepository.GetAllList();
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

            await _organizationUnitManager.CreateAsync(orgUnit);
            return "";
        }

        public async Task UpdateUnitOrder(List<OrderOrganizationUnitInput> input)
        {
            foreach (var orderOrganizationUnitInput in input)
            {
                var element = _organizationUniRepository.Get(orderOrganizationUnitInput.id);
                if (orderOrganizationUnitInput.children == null) continue;
                if (orderOrganizationUnitInput.children.Any())
                {
                    await UpdateChilds(orderOrganizationUnitInput.children, element.Id);
                }
            }
        }

        public async Task<OrganizationUnitDto> GetOrganizationUnit(long id)
        {
            var unit = await _organizationUniRepository.GetAsync(id);
            return unit.MapTo<OrganizationUnitDto>();
        }

        public async Task DeleteUnit(long unitId)
        {
            //Check
            await _organizationUnitManager. DeleteAsync(unitId);
        }

        public async Task EditUnit(OrganizationUnitDto model)
        {
            _organizationUnitsPolicies.AttempEditPolicy(model);
            var orgUnit = _organizationUniRepository.Get(model.Id);
            orgUnit.DisplayName = model.DisplayName;
            await _organizationUnitManager.UpdateAsync(orgUnit);
        }

        public async Task TurnToRoot(OrganizationUnitDto model)
        {
            //Policy of any type?
            await _organizationUnitManager.MoveAsync(model.Id,null);
        }

        public async Task<OrganizationUnit> GetOrganizationUnitAsync(long orgId)
        {
            return await _organizationUniRepository.GetAsync(orgId);
        }

        public OrganizationUnit GetOrganizationUnitByName(string name, int? tenantId)
        {
            return  _organizationUniRepository.FirstOrDefault(a => a.DisplayName == name);
        }

        private async Task UpdateChilds(IEnumerable<OrderOrganizationUnitInput> children, long elementId)
        {
            foreach (var organizationUnitInput in children)
            {
                await _organizationUnitManager.MoveAsync(organizationUnitInput.id, elementId);
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
            await _organizationUnitManager.CreateAsync(orgUnit);
        }
    }
}

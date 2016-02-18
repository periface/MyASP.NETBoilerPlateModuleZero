using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Organizations;
using Abp.UI;

namespace SimpleCms.ModuleZero.Managers
{
    public class OrganizationManager : OrganizationUnitManager, IOrganizationManager
    {

        public OrganizationManager(IRepository<OrganizationUnit, long> organizationUnitRepository) : base(organizationUnitRepository)
        {

        }

        public async Task ConvertUnitToRoot(OrganizationUnit input)
        {

            await MoveAsync(input.Id, null);
        }

        public async Task CreateOrganizationalUnit(OrganizationUnit input)
        {

            await ValidateOrganizationUnitAsync(input);

            await CreateAsync(input);

        }

        public async Task<string> CreateOrganizationalUnitGetId(OrganizationUnit input)
        {
            await ValidateOrganizationUnitAsync(input);
            await CreateAsync(input);
            return input.Code;
        }

        public async Task EditOrganizationUnit(OrganizationUnit input)
        {
            await UpdateAsync(input);
        }

        public async Task ConvertToRootAsync(long id)
        {
            await MoveAsync(id,null);
        }

        public async Task<OrganizationUnit> GetOrganizationUnitAsync(long id)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id);
            return ou;
        }

        public OrganizationUnit GetOrganizationUnitByName(string name, int? tenantId)
        {
            return OrganizationUnitRepository.FirstOrDefault(a => a.DisplayName.ToUpper() == name.ToUpper() && a.TenantId == tenantId);
        }
    }
}

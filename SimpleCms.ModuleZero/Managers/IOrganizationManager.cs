using System.Threading.Tasks;
using Abp.Organizations;

namespace SimpleCms.ModuleZero.Managers
{
    public interface IOrganizationManager
    {
        Task CreateOrganizationalUnit(OrganizationUnit input);
        Task<string> CreateOrganizationalUnitGetId(OrganizationUnit input);
        Task<OrganizationUnit> GetOrganizationUnitAsync(long id);
        OrganizationUnit GetOrganizationUnitByName(string name,int? tenantId);
        Task ConvertUnitToRoot(OrganizationUnit input);
        Task EditOrganizationUnit(OrganizationUnit input);
        Task ConvertToRootAsync(long id);
    }
}
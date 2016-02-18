using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.UI;
using SimpleCms.ModuleZero.Tenancy.Dto;
using SimpleCms.MultiTenancy;

namespace SimpleCms.ModuleZero.Tenancy
{
    public class TenancyService : ITenancyService
    {
        private readonly TenantManager _tenantManager;

        public TenancyService(TenantManager tenantManager)
        {
            _tenantManager = tenantManager;
        }

        public void ChangeTenancyName(ChangeTenancyNameInput input)
        {
            throw new NotImplementedException();
        }

        public void DeleteTenancy(DeleteTenancyInput input)
        {
            throw new NotImplementedException();
        }

        public async Task< int?> GetTenantByName(string tenancyName)
        {
            var tenancy = await _tenantManager.FindByTenancyNameAsync(tenancyName);
            return tenancy?.Id;
        }
    }
}

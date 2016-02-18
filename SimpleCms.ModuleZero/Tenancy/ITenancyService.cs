using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using SimpleCms.ModuleZero.Tenancy.Dto;

namespace SimpleCms.ModuleZero.Tenancy
{
    public interface ITenancyService : IApplicationService
    {
        void ChangeTenancyName(ChangeTenancyNameInput input);
        /// <summary>
        /// Deletes the current tenancy
        /// </summary>
        void DeleteTenancy(DeleteTenancyInput input);

        Task<int?> GetTenantByName(string tenancyName);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;
using SimpleCms.ModuleZero.OrgUnits.Dto;

namespace SimpleCms.ModuleZero.Policies
{
    public interface IOrganizationUnitsPolicies : IDomainService
    {
        void AttempCreatePolicy(OrganizationUnitInput input);
        void AttempEditPolicy(OrganizationUnitDto input);
    }
}

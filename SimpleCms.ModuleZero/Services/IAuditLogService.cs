using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Auditing;
using SimpleCms.ModuleZero.GenericOutPuts;

namespace SimpleCms.ModuleZero.Services
{
    public interface IAuditLogService : IApplicationService
    {
        JqGridObject GetAuditLogsFromTenant(int tenantId);
    }
}

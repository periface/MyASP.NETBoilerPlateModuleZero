using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Auditing;
using Abp.Domain.Repositories;
using Abp.Threading;
using SimpleCms.ModuleZero.GenericOutPuts;
using SimpleCms.Users;

namespace SimpleCms.ModuleZero.Services
{
    public class AuditLogService : SimpleCmsAppServiceBase, IAuditLogService
    {
        private readonly IRepository<AuditLog, long> _repository;
        private readonly UserManager _userManager;
        public AuditLogService(IRepository<AuditLog, long> repository, UserManager userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public JqGridObject GetAuditLogsFromTenant(int tenantId)
        {
            var userName = "Empty";
            var auditLogs = _repository.GetAll().Where(a => a.TenantId == tenantId).OrderByDescending(a=>a.ExecutionTime).Take(10).ToList();
            var data = new List<AuditLogOutPut>();
            foreach (var auditLog in auditLogs)
            {
                if (auditLog.UserId != null)
                {
                    var user = AsyncHelper.RunSync(() => _userManager.GetUserByIdAsync(auditLog.UserId.Value));
                    //    var user = await _userManager.GetUserByIdAsync(auditLog.UserId.Value);
                    if (user != null)
                    {
                        userName = user.UserName;
                    }
                }
                var last = auditLog.ServiceName.Split('.').Last();
                data.Add(new AuditLogOutPut()
                {
                    Id = auditLog.Id,
                    Browser = auditLog.BrowserInfo,
                    Action = auditLog.MethodName,
                    IpAdress = auditLog.ClientIpAddress,
                    Duration = auditLog.ExecutionDuration + " ms",
                    Service = last,
                    Time = auditLog.ExecutionTime.ToShortDateString(),
                    UserName = userName,
                });
            }
            return JqGridObject.CreateModel(2, data.Count, 10, "Id", "asc", data);
        }
    }
}

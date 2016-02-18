using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Services;
using SimpleCms.VimeApp.VimeAppEntities;

namespace SimpleCms.VimeApp.Policies
{
    public interface IVimeAppInfoCreationPolicy : IDomainService
    {
        void CheckCreationAttemptAsync(VimeAppInfo info);
        void CheckExistingName(VimeAppInfo info);
    }
}

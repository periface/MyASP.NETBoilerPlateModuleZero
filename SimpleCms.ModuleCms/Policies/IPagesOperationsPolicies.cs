using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;
using SimpleCms.ModuleCms.Entities;

namespace SimpleCms.ModuleCms.Policies
{
    public interface IPagesOperationsPolicies : IDomainService
    {
        void AttemptPageCreationAsync(Page page);
        void AttemptPageDeleteAsync(Page page);
        void AttemptAddContentToPageAsync(Page page);
    }
}

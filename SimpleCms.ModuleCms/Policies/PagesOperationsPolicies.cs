using Abp.Domain.Services;
using Abp.UI;
using SimpleCms.ModuleCms.Entities;

namespace SimpleCms.ModuleCms.Policies
{
    public class PagesOperationsPolicies :DomainService, IPagesOperationsPolicies
    {
        public void AttemptPageCreationAsync(Page page)
        {
            return;
        }

        public void AttemptPageDeleteAsync(Page page)
        {
            return;
        }

        public void AttemptAddContentToPageAsync(Page page)
        {
        }
    }
}

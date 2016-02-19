using Abp.Web.Mvc.Views;
using SimpleCms.Web.Views;

namespace SimpleCms.Web.Areas.AdminCms.Views
{
    public abstract class SimpleCmsAdminCmsWebViewPageBase : SimpleCmsWebViewPageBase<dynamic>
    {

    }

    public abstract class SimpleCmsAdminCmsWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected SimpleCmsAdminCmsWebViewPageBase()
        {
            /*LocalizationSourceName = VimeApp.SimpleCmsVimeAppConstants.LocalizationSourceName*/;
            LocalizationSourceName =ModuleCms.Constants.ModuleCmsConstants.Source;
            
        }
    }
}
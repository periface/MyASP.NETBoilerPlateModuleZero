using Abp.Web.Mvc.Views;

namespace SimpleCms.Web.Views
{
    public abstract class SimpleCmsWebViewPageBase : SimpleCmsWebViewPageBase<dynamic>
    {

    }

    public abstract class SimpleCmsWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected SimpleCmsWebViewPageBase()
        {
            LocalizationSourceName = SimpleCmsConsts.LocalizationSourceName;
        }
    }
}
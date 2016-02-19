using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Abp.Web.Mvc.Views;
using SimpleCms.Web.Views;

namespace SimpleCms.Web.Areas.Admin.Views
{
    public abstract class SimpleCmsAdminWebViewPageBase : SimpleCmsWebViewPageBase<dynamic>
    {

    }

    public abstract class SimpleCmsAdminWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected SimpleCmsAdminWebViewPageBase()
        {
            /*LocalizationSourceName = VimeApp.SimpleCmsVimeAppConstants.LocalizationSourceName*/;
            LocalizationSourceName = ModuleZero.Constants.ModuleZeroConstants.Source;
            
        }
    }
}
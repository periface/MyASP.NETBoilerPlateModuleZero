using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Authorization;
using Abp.Localization;
using Abp.Zero.Configuration;
using Abp.Modules;
using Abp.Notifications;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using SimpleCms.Api;

namespace SimpleCms.Web
{
    [DependsOn(
        typeof(SimpleCmsDataModule),
        typeof(SimpleCmsApplicationModule),
        typeof(SimpleCmsWebApiModule),
        typeof(AbpWebMvcModule),
        typeof(AbpWebSignalRModule))]
    public class SimpleCmsWebModule : AbpModule
    {
        public override void PreInitialize()
        {
           
            //Enable database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<SimpleCmsNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
    
}

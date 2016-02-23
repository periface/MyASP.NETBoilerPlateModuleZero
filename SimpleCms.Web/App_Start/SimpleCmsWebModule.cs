using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Authorization;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Localization;
using Abp.Zero.Configuration;
using Abp.Modules;
using Abp.Notifications;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Hangfire;
using SimpleCms.Api;

namespace SimpleCms.Web
{
    [DependsOn(
        typeof(SimpleCmsDataModule),
        typeof(SimpleCmsApplicationModule),
        typeof(SimpleCmsWebApiModule),
        typeof(AbpWebSignalRModule),
        typeof(AbpHangfireModule),
        typeof(AbpWebMvcModule))]
    public class SimpleCmsWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Notifications.Providers.Add<MyAppNotificationProvider>();
            //Enable database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<SimpleCmsNavigationProvider>();
            Configuration.BackgroundJobs.UseHangfire(configuration =>
            {
                configuration.GlobalConfiguration.UseSqlServerStorage("Default");
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        
    }
    public class MyAppNotificationProvider : NotificationProvider
    {
        public override void SetNotifications(INotificationDefinitionContext context)
        {
            context.Manager.Add(
                new NotificationDefinition(
                    "CreatedRole"
                    )
                );
        }
    }
}

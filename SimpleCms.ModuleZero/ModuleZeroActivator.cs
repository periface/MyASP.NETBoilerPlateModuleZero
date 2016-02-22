using System.Reflection;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Notifications;
using Abp.Web.SignalR;
using SimpleCms.ModuleZero.Constants;

namespace SimpleCms.ModuleZero
{
    [DependsOn(typeof(AbpWebSignalRModule))]
    public class ModuleZeroActivator : AbpModule
    {

        public override void Initialize()
        {

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PreInitialize()
        {
            Configuration.Notifications.Providers.Add<MyAppNotificationProvider>();
            Configuration.Authorization.Providers.Add<ModuleZeroPermissionsProvider>();
            Configuration.Navigation.Providers.Add<ModuleZeroMenuProvider>();
            //Configuration.Navigation.Providers.Add<ModuleMenuProvider>();
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(ModuleZeroConstants.Source,
                new XmlEmbeddedFileLocalizationDictionaryProvider(
                    Assembly.GetExecutingAssembly(),
                    "SimpleCms.ModuleZero.Localization.Source")));
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
}

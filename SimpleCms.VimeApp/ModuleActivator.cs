using System.Reflection;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;

namespace SimpleCms.VimeApp
{
    public class ModuleActivator : AbpModule
    {

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PreInitialize()
        {
            //Configuration.Navigation.Providers.Add<ModuleMenuProvider>();
            //Configuration.Localization.Sources.Add(
            //    new DictionaryBasedLocalizationSource(SimpleCmsVimeAppConstants.LocalizationSourceName,
            //    new XmlEmbeddedFileLocalizationDictionaryProvider(
            //        Assembly.GetExecutingAssembly(),
            //        "SimpleCms.VimeApp.Localization.Source")));
        }
    }
}

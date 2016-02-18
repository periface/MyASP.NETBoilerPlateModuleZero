using System.Reflection;
using Abp.Application.Features;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using SimpleCms.ModuleCms.Constants;
using SimpleCms.ModuleZero;

namespace SimpleCms.ModuleCms
{
    [DependsOn(typeof(ModuleZeroActivator))]
    public class ModuleCmsActivator : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PreInitialize()
        {
            Configuration.Features.Providers.Add<ModuleCmsFeatureProvider>();
            Configuration.Navigation.Providers.Add<ModuleCmsMenuProvider>();
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(ModuleCmsConstants.Source,
                new XmlEmbeddedFileLocalizationDictionaryProvider(
                    Assembly.GetExecutingAssembly(),
                    "SimpleCms.ModuleCms.Localization.Source")));
        }
    }

    public class ModuleCmsFeatureProvider : FeatureProvider
    {
        

        public override void SetFeatures(IFeatureDefinitionContext context)
        {
            context.Create("CmsSystem", "true");
        }
    }
}

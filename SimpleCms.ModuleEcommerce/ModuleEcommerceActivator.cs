using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using SimpleCms.ModuleEcommerce.Constants;
using SimpleCms.ModuleZero;

namespace SimpleCms.ModuleEcommerce
{
    [DependsOn(typeof(ModuleZeroActivator))]
    public class ModuleEcommerceActivator : AbpModule
    {
        public override void Initialize()
        {
            Configuration.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<ModuleEcommerceMenuProvider>();
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(ModuleEcommerceConstants.Source,
                new XmlEmbeddedFileLocalizationDictionaryProvider(
                    Assembly.GetExecutingAssembly(),
                    "SimpleCms.ModuleEcommerce.Localization.Source")));
        }
    }
}

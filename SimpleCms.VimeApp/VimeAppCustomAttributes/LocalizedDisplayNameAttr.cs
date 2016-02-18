using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Abp.Localization;

namespace SimpleCms.VimeApp.VimeAppCustomAttributes
{
    public class LocalizedDisplayNameAttr : DisplayNameAttribute
    {
        public LocalizedDisplayNameAttr(string resourceId) : base(GetMessageFromResource(resourceId))
        {
            ResourceKey = resourceId;
        }
        public string ResourceKey { get; set; }

        public override string DisplayName => GetMessageFromResource(ResourceKey);

        private static string GetMessageFromResource(string resourceId)
        {
            var ioc = Abp.Dependency.IocManager.Instance;
            var localizationManager = ioc.IocContainer.Resolve<ILocalizationManager>();
            var value = localizationManager.GetString(SimpleCmsVimeAppConstants.LocalizationSourceName, resourceId, Thread.CurrentThread.CurrentUICulture);
            return value;
        }
    }
}

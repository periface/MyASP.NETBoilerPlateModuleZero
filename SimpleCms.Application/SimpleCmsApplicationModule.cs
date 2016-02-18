using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace SimpleCms
{
    [DependsOn(typeof(SimpleCmsCoreModule), typeof(AbpAutoMapperModule))]
    public class SimpleCmsApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using SimpleCms.EntityFramework;

namespace SimpleCms
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(SimpleCmsCoreModule))]
    public class SimpleCmsDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

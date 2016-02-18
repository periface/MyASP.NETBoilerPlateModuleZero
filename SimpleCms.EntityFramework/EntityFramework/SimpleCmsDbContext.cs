using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using SimpleCms.Authorization.Roles;
using SimpleCms.ModuleCms.Entities;
using SimpleCms.ModuleCms.Themes.Dto;
using SimpleCms.MultiTenancy;
using SimpleCms.Users;
namespace SimpleCms.EntityFramework
{
    public class SimpleCmsDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        public virtual IDbSet<Page> Pages { get; set; }
        public virtual IDbSet<PageTags> Tags { get; set; }
        public virtual IDbSet<Menu> Menus { get; set; }
        public virtual IDbSet<PageCategory> PageCategory { get; set; }
        public virtual IDbSet<SiteConfig> SieConfigs { get; set; }
        public virtual IDbSet<SiteAdress> SiteAdresses { get; set; }
        public virtual IDbSet<SiteInfo> SiteInfos { get; set; }
        public virtual IDbSet<Theme> Themes { get; set; }
        public virtual IDbSet<ConfigThemeRelation> Config { get; set; }
        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public SimpleCmsDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in SimpleCmsDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of SimpleCmsDbContext since ABP automatically handles it.
         */
        public SimpleCmsDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public SimpleCmsDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}

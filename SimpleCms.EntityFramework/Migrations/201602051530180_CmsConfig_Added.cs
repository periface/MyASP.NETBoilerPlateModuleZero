using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace SimpleCms.Migrations
{
    public partial class CmsConfig_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SiteConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsEnabled = c.Boolean(nullable: false),
                        AllowUsersRegistration = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SiteConfig_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SiteConfig_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ThemeName = c.String(),
                        ThemeDescription = c.String(),
                        ThemePreview = c.String(),
                        UniqueFolderId = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Theme_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SiteAdresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdressL1 = c.String(),
                        AdressL2 = c.String(),
                        PhoneNumber = c.String(),
                        Lat = c.String(),
                        Lon = c.String(),
                        TenantId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SiteAdress_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SiteAdress_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SiteInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteTitle = c.String(),
                        SiteName = c.String(),
                        SiteLogo = c.String(),
                        SiteIcon = c.String(),
                        SiteSlogan = c.String(),
                        SiteDescription = c.String(),
                        TenantId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SiteInfo_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SiteInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ThemeSiteConfigs",
                c => new
                    {
                        Theme_Id = c.Int(nullable: false),
                        SiteConfig_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Theme_Id, t.SiteConfig_Id })
                .ForeignKey("dbo.Themes", t => t.Theme_Id, cascadeDelete: true)
                .ForeignKey("dbo.SiteConfigs", t => t.SiteConfig_Id, cascadeDelete: true)
                .Index(t => t.Theme_Id)
                .Index(t => t.SiteConfig_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThemeSiteConfigs", "SiteConfig_Id", "dbo.SiteConfigs");
            DropForeignKey("dbo.ThemeSiteConfigs", "Theme_Id", "dbo.Themes");
            DropIndex("dbo.ThemeSiteConfigs", new[] { "SiteConfig_Id" });
            DropIndex("dbo.ThemeSiteConfigs", new[] { "Theme_Id" });
            DropTable("dbo.ThemeSiteConfigs");
            DropTable("dbo.SiteInfoes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SiteInfo_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SiteInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.SiteAdresses",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SiteAdress_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SiteAdress_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Themes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Theme_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.SiteConfigs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SiteConfig_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SiteConfig_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}

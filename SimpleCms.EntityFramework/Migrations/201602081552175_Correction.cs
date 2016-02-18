namespace SimpleCms.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Correction : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ThemeSiteConfigs", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.ThemeSiteConfigs", "SiteConfig_Id", "dbo.SiteConfigs");
            DropIndex("dbo.ThemeSiteConfigs", new[] { "Theme_Id" });
            DropIndex("dbo.ThemeSiteConfigs", new[] { "SiteConfig_Id" });
            CreateTable(
                "dbo.ConfigThemeRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdConfig = c.Int(nullable: false),
                        IdTheme = c.Int(nullable: false),
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
                    { "DynamicFilter_ConfigThemeRelation_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SiteConfigs", t => t.IdConfig, cascadeDelete: true)
                .ForeignKey("dbo.Themes", t => t.IdTheme, cascadeDelete: true)
                .Index(t => t.IdConfig)
                .Index(t => t.IdTheme);
            
            DropTable("dbo.ThemeSiteConfigs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ThemeSiteConfigs",
                c => new
                    {
                        Theme_Id = c.Int(nullable: false),
                        SiteConfig_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Theme_Id, t.SiteConfig_Id });
            
            DropForeignKey("dbo.ConfigThemeRelations", "IdTheme", "dbo.Themes");
            DropForeignKey("dbo.ConfigThemeRelations", "IdConfig", "dbo.SiteConfigs");
            DropIndex("dbo.ConfigThemeRelations", new[] { "IdTheme" });
            DropIndex("dbo.ConfigThemeRelations", new[] { "IdConfig" });
            DropTable("dbo.ConfigThemeRelations",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConfigThemeRelation_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            CreateIndex("dbo.ThemeSiteConfigs", "SiteConfig_Id");
            CreateIndex("dbo.ThemeSiteConfigs", "Theme_Id");
            AddForeignKey("dbo.ThemeSiteConfigs", "SiteConfig_Id", "dbo.SiteConfigs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ThemeSiteConfigs", "Theme_Id", "dbo.Themes", "Id", cascadeDelete: true);
        }
    }
}

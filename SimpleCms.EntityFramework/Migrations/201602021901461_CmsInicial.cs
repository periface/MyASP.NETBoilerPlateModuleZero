namespace SimpleCms.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class CmsInicial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LandingPageSections", "LandingPageConfiguration_Id", "dbo.LandingPages");
            DropIndex("dbo.LandingPageSections", new[] { "LandingPageConfiguration_Id" });
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ShortDescription = c.String(),
                        Content = c.String(),
                        TenantId = c.Int(nullable: false),
                        FriendlyUrl = c.String(),
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
                    { "DynamicFilter_Page_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Page_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PageTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(nullable: false),
                        Tag = c.String(),
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
                    { "DynamicFilter_PageTags_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PageTags_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PageTagsPages",
                c => new
                    {
                        PageTags_Id = c.Int(nullable: false),
                        Page_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PageTags_Id, t.Page_Id })
                .ForeignKey("dbo.PageTags", t => t.PageTags_Id, cascadeDelete: true)
                .ForeignKey("dbo.Pages", t => t.Page_Id, cascadeDelete: true)
                .Index(t => t.PageTags_Id)
                .Index(t => t.Page_Id);
            
            DropTable("dbo.VimeAppInfoes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VimeAppInfo_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VimeAppInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.LandingPages",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_LandingPage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.LandingPageSections");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LandingPageSections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionContent = c.String(),
                        SectionTitle = c.String(),
                        SectionImage = c.String(),
                        SectionContentImagePosition = c.String(),
                        SectionType = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        LandingPageConfiguration_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LandingPages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ConfigurationName = c.String(),
                        ConfigurationNotes = c.String(),
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
                    { "DynamicFilter_LandingPage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VimeAppInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SiteName = c.String(),
                        SiteMision = c.String(),
                        SiteVision = c.String(),
                        SiteMainIcon = c.String(),
                        SiteLogo = c.String(),
                        SiteSlogan = c.String(),
                        IsActive = c.Boolean(nullable: false),
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
                    { "DynamicFilter_VimeAppInfo_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VimeAppInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.PageTagsPages", "Page_Id", "dbo.Pages");
            DropForeignKey("dbo.PageTagsPages", "PageTags_Id", "dbo.PageTags");
            DropIndex("dbo.PageTagsPages", new[] { "Page_Id" });
            DropIndex("dbo.PageTagsPages", new[] { "PageTags_Id" });
            DropTable("dbo.PageTagsPages");
            DropTable("dbo.PageTags",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PageTags_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PageTags_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Pages",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Page_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Page_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            CreateIndex("dbo.LandingPageSections", "LandingPageConfiguration_Id");
            AddForeignKey("dbo.LandingPageSections", "LandingPageConfiguration_Id", "dbo.LandingPages", "Id");
        }
    }
}

namespace SimpleCms.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryContents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Lang = c.String(),
                        CategoryName = c.String(),
                        FriendlyUrl = c.String(),
                        TenantId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Category_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CategoryContent_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CategoryContent_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PageCategories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.PageContents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        FriendlyUrl = c.String(),
                        Lang = c.String(),
                        ShortDescription = c.String(),
                        Content = c.String(),
                        TenantId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Page_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PageContent_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PageContent_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pages", t => t.Page_Id)
                .Index(t => t.Page_Id);
            
            DropColumn("dbo.PageCategories", "CategoryName");
            DropColumn("dbo.Pages", "Title");
            DropColumn("dbo.Pages", "ShortDescription");
            DropColumn("dbo.Pages", "Content");
            DropColumn("dbo.Pages", "FriendlyUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pages", "FriendlyUrl", c => c.String());
            AddColumn("dbo.Pages", "Content", c => c.String());
            AddColumn("dbo.Pages", "ShortDescription", c => c.String());
            AddColumn("dbo.Pages", "Title", c => c.String());
            AddColumn("dbo.PageCategories", "CategoryName", c => c.String());
            DropForeignKey("dbo.PageContents", "Page_Id", "dbo.Pages");
            DropForeignKey("dbo.CategoryContents", "Category_Id", "dbo.PageCategories");
            DropIndex("dbo.PageContents", new[] { "Page_Id" });
            DropIndex("dbo.CategoryContents", new[] { "Category_Id" });
            DropTable("dbo.PageContents",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PageContent_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PageContent_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CategoryContents",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CategoryContent_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CategoryContent_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}

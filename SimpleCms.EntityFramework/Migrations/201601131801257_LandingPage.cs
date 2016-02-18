namespace SimpleCms.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class LandingPage : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LandingPages", t => t.LandingPageConfiguration_Id)
                .Index(t => t.LandingPageConfiguration_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LandingPageSections", "LandingPageConfiguration_Id", "dbo.LandingPages");
            DropIndex("dbo.LandingPageSections", new[] { "LandingPageConfiguration_Id" });
            DropTable("dbo.LandingPageSections");
            DropTable("dbo.LandingPages",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_LandingPage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}

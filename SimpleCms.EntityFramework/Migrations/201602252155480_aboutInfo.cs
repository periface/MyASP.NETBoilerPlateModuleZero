namespace SimpleCms.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class aboutInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AboutInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Language = c.String(),
                        Mision = c.String(),
                        Vision = c.String(),
                        QualityPolitic = c.String(),
                        DisplayQ = c.Boolean(nullable: false),
                        PrivacyPolitic = c.String(),
                        DisplayP = c.Boolean(nullable: false),
                        Objetives = c.String(),
                        DisplayO = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        SiteInfo_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AboutInfo_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AboutInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SiteInfoes", t => t.SiteInfo_Id)
                .Index(t => t.SiteInfo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AboutInfoes", "SiteInfo_Id", "dbo.SiteInfoes");
            DropIndex("dbo.AboutInfoes", new[] { "SiteInfo_Id" });
            DropTable("dbo.AboutInfoes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AboutInfo_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AboutInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}

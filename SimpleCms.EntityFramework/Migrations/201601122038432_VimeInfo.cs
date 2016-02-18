namespace SimpleCms.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class VimeInfo : DbMigration
    {
        public override void Up()
        {
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
                    { "DynamicFilter_VimeAppInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VimeAppInfoes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VimeAppInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}

namespace SimpleCms.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class ContenidoSeparado : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
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
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_VimeAppInfo_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AddColumn("dbo.VimeAppInfoes", "TenantId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VimeAppInfoes", "TenantId");
            AlterTableAnnotations(
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
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_VimeAppInfo_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}

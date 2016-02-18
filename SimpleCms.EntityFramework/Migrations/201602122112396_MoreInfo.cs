namespace SimpleCms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteInfoes", "SitePhone", c => c.String());
            AddColumn("dbo.SiteInfoes", "SiteMail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteInfoes", "SiteMail");
            DropColumn("dbo.SiteInfoes", "SitePhone");
        }
    }
}

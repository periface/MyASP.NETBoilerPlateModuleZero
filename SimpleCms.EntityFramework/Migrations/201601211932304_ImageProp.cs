namespace SimpleCms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpUsers", "UrlImageAvatar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpUsers", "UrlImageAvatar");
        }
    }
}

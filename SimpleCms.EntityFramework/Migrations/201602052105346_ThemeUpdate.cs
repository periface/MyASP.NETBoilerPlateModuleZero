namespace SimpleCms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemeUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Themes", "IsUnderConstruction", c => c.Boolean(nullable: false));
            AddColumn("dbo.Themes", "IsAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Themes", "IsAvailable");
            DropColumn("dbo.Themes", "IsUnderConstruction");
        }
    }
}

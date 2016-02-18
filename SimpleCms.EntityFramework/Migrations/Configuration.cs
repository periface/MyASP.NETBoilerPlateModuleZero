using System.Data.Entity.Migrations;
using SimpleCms.Migrations.SeedData;

namespace SimpleCms.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SimpleCms.EntityFramework.SimpleCmsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SimpleCms";
        }

        protected override void Seed(SimpleCms.EntityFramework.SimpleCmsDbContext context)
        {
            new InitialDataBuilder(context).Build();
        }
    }
}

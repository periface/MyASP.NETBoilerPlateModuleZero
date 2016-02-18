using SimpleCms.EntityFramework;
using EntityFramework.DynamicFilters;

namespace SimpleCms.Migrations.SeedData
{
    public class InitialDataBuilder
    {
        private readonly SimpleCmsDbContext _context;

        public InitialDataBuilder(SimpleCmsDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            _context.DisableAllFilters();

            new DefaultEditionsBuilder(_context).Build();
            new DefaultTenantRoleAndUserBuilder(_context).Build();
            new DefaultLanguagesBuilder(_context).Build();
        }
    }
}

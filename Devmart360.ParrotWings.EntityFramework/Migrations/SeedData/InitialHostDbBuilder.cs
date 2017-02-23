using Devmart360.ParrotWings.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Devmart360.ParrotWings.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly ParrotWingsDbContext _context;

        public InitialHostDbBuilder(ParrotWingsDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}

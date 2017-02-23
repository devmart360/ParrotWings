using System.Linq;
using Devmart360.ParrotWings.EntityFramework;
using Devmart360.ParrotWings.MultiTenancy;

namespace Devmart360.ParrotWings.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly ParrotWingsDbContext _context;

        public DefaultTenantCreator(ParrotWingsDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}

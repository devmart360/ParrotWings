using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using Devmart360.ParrotWings.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace Devmart360.ParrotWings.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ParrotWings.EntityFramework.ParrotWingsDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ParrotWings";
        }

        protected override void Seed(ParrotWings.EntityFramework.ParrotWingsDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}

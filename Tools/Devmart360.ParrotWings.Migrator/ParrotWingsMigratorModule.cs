using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Devmart360.ParrotWings.EntityFramework;

namespace Devmart360.ParrotWings.Migrator
{
    [DependsOn(typeof(ParrotWingsDataModule))]
    public class ParrotWingsMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<ParrotWingsDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
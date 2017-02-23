using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using Devmart360.ParrotWings.EntityFramework;

namespace Devmart360.ParrotWings
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(ParrotWingsCoreModule))]
    public class ParrotWingsDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ParrotWingsDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

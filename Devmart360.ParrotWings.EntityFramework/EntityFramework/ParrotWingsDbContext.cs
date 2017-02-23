using System.Data.Common;
using Abp.Zero.EntityFramework;
using Devmart360.ParrotWings.Authorization.Roles;
using Devmart360.ParrotWings.MultiTenancy;
using Devmart360.ParrotWings.Users;

namespace Devmart360.ParrotWings.EntityFramework
{
    public class ParrotWingsDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public ParrotWingsDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in ParrotWingsDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of ParrotWingsDbContext since ABP automatically handles it.
         */
        public ParrotWingsDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public ParrotWingsDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}

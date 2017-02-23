using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Devmart360.ParrotWings.EntityFramework.Repositories
{
    public abstract class ParrotWingsRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<ParrotWingsDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected ParrotWingsRepositoryBase(IDbContextProvider<ParrotWingsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class ParrotWingsRepositoryBase<TEntity> : ParrotWingsRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected ParrotWingsRepositoryBase(IDbContextProvider<ParrotWingsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}

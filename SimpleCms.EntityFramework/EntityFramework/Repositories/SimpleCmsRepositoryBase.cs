using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace SimpleCms.EntityFramework.Repositories
{
    public abstract class SimpleCmsRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<SimpleCmsDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected SimpleCmsRepositoryBase(IDbContextProvider<SimpleCmsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }

    public abstract class SimpleCmsRepositoryBase<TEntity> : SimpleCmsRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected SimpleCmsRepositoryBase(IDbContextProvider<SimpleCmsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}

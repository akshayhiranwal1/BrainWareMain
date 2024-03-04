using System;
using Api.Interfaces;

namespace Api.Infrastructure.Repository
{
	public class UnitOfWork : IUnitOfWork
    {
        private IDbContext dbContext;
        private Dictionary<Type, object> repositories;

        public UnitOfWork(IDbContext context)
        {
            dbContext = context;
        }

        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!repositories.ContainsKey(type))
            {
                repositories[type] = new Repository<TEntity>(dbContext);
            }

            return (IRepository<TEntity>)repositories[type];
        }

    }
}

using System;
namespace Api.Infrastructure.Repository
{
	public interface IUnitOfWork
	{
        IRepository<TEntity> GetRepository<TEntity>()
         where TEntity : class;
    }
}


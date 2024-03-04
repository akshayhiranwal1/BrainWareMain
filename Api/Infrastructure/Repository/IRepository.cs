using System;
using System.Linq.Expressions;

namespace Api.Infrastructure.Repository
{
	public interface IRepository<T>
	{
        List<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string include, string include1 = null);
    }
}


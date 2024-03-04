using System;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Infrastructure.Repository
{
	public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly IDbContext _context;

        private readonly DbSet<T> dbSet;
        public Repository(IDbContext context)
        {
            this._context = context;
            dbSet = _context.Set<T>();
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        /// <inheritdoc />
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        /// <inheritdoc />
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string include, string include1 = null)
        {
            if (!string.IsNullOrEmpty(include) && !string.IsNullOrEmpty(include1))
                return dbSet.Include(include).Include(include1).Where(predicate);

            return dbSet.Where(predicate).Include(include);
        }
    }
}


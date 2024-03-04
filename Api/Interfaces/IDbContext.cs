using Microsoft.EntityFrameworkCore;

namespace Api.Interfaces
{
	public interface IDbContext
	{
        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;
    }
}


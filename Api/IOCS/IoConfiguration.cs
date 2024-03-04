using System;
using Api.Infrastructure;
using Api.Infrastructure.Repository;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.IOCS
{
	public static class IoConfiguration
	{
		public static void ConfigureService(IServiceCollection services)
		{
			services.AddDbContext<BrainWareContext>(i => i.UseSqlServer("Server=localhost,1433;Initial Catalog=BrainWare;User Id=sa;Password=pa55w0rd!;TrustServerCertificate=true;"));
			services.AddScoped(typeof(IDbContext), typeof(BrainWareContext));

			services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
			services.AddTransient<IUnitOfWork, UnitOfWork>();
			services.AddTransient<IOrder, OrderService>();
		}
	}
}


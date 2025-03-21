using Microsoft.Extensions.DependencyInjection;
using PRN222.lab2.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.lab2.Repositories.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly MyStoreDbContext _context;
		private readonly IServiceProvider _serviceProvider;

		public UnitOfWork(MyStoreDbContext context, IServiceProvider serviceProvider)
		{
			_context = context;
			_serviceProvider = serviceProvider;
		}

		public IGenericRepository<T> Repository<T>() where T : class
		{
			return _serviceProvider.GetRequiredService<IGenericRepository<T>>();
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}

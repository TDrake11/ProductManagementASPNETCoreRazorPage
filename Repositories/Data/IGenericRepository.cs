using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.lab2.Repositories.Data
{
	public interface IGenericRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetListAsync(
			Expression<Func<T, bool>>? filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			int? page = null,
			int? pageSize = null,
			string? includeProperties = null
		);

		Task<T?> GetByIdAsync(int id);
		Task AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(int id);
	}
}

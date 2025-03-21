using Microsoft.EntityFrameworkCore;
using PRN222.lab2.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.lab2.Repositories.Data
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly MyStoreDbContext _context;
		private readonly DbSet<T> _dbSet;

		public GenericRepository(MyStoreDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public async Task<IEnumerable<T>> GetListAsync(
			Expression<Func<T, bool>>? filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			int? page = null,
			int? pageSize = null,
			string? includeProperties = null // format: "Property1,Property2"
		)
		{
			IQueryable<T> query = _dbSet;

			// Filter (Search)(Where)
			if (filter != null)
			{
				query = query.Where(filter);
			}

			// Include related entities
			if (!string.IsNullOrWhiteSpace(includeProperties))
			{
				foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty);
				}
			}

			// Sorting
			if (orderBy != null)
			{
				query = orderBy(query);
			}

			// Paging
			if (page.HasValue && pageSize.HasValue)
			{
				int skip = (page.Value - 1) * pageSize.Value;
				query = query.Skip(skip).Take(pageSize.Value);
			}

			return await query.ToListAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public async Task UpdateAsync(T entity)
		{
			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await GetByIdAsync(id);
			if (entity != null)
			{
				_dbSet.Remove(entity);
			}
		}
	}
}

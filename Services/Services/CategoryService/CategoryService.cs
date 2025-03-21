using PRN222.lab2.Repositories.Data;
using PRN222.lab2.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Lab2.Services.Services.CategoryService
{
	public class CategoryService : ICategoryService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoryService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IEnumerable<Category>> GetListCategories()
		{
			return await _unitOfWork.Repository<Category>().GetListAsync();
		}
	}
}

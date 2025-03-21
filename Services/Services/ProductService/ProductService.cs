using PRN222.lab2.Repositories.Data;
using PRN222.lab2.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PRN222.Lab2.Services.Services.ProductService
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task DeleteProduct(int id)
		{
			await _unitOfWork.Repository<Product>().DeleteAsync(id);
			_unitOfWork.SaveChanges();
		}

		public async Task<Product?> GetProductById(int id)
		{
			var products = await _unitOfWork.Repository<Product>()
				.GetListAsync(
					filter: p => p.ProductId == id,
					includeProperties: "Category"
				);

			return products.FirstOrDefault();
		}

		public async Task<IEnumerable<Product>> GetListProducts(string? search, string? sort, int? page, int? pageSize, string? include)
		{
			if (!IsValidIncludeProperties(include))
			{
				throw new ArgumentException("Invalid include properties format.");
			}

			// Sort order
			Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = q => q.OrderBy(p => p.ProductName);
			switch (sort)
			{
				case "name_asc":
					orderBy = q => q.OrderBy(p => p.ProductName);
					break;
				case "name_desc":
					orderBy = q => q.OrderByDescending(p => p.ProductName);
					break;
				case "price_asc":
					orderBy = q => q.OrderBy(p => p.UnitPrice);
					break;
				case "price_desc":
					orderBy = q => q.OrderByDescending(p => p.UnitPrice);
					break;
			}

			return await _unitOfWork.Repository<Product>().GetListAsync(
				filter: p => string.IsNullOrEmpty(search) || p.ProductName.Contains(search),
				orderBy: orderBy,
				page: page,
				pageSize: pageSize,
				includeProperties: include
			);
		}


		// Helper method to validate include properties format
		private bool IsValidIncludeProperties(string? includeProperties)
		{
			if (string.IsNullOrWhiteSpace(includeProperties))
				return true; // Allow empty include

			// Regex to match valid format: Property1,Property2,...
			return Regex.IsMatch(includeProperties, @"^[A-Za-z0-9_]+(,[A-Za-z0-9_]+)*$");
		}

		public async Task CreateProduct(Product product)
		{
			await _unitOfWork.Repository<Product>().AddAsync(product);
			_unitOfWork.SaveChanges();
		}

		public async Task UpdateProduct(Product product)
		{
			await _unitOfWork.Repository<Product>().UpdateAsync(product);
			_unitOfWork.SaveChanges();
		}
	}
}

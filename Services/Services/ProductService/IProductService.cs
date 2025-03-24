using PRN222.lab2.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Lab2.Services.Services.ProductService
{
	public interface IProductService
	{
		Task CreateProduct(Product product);
		Task DeleteProduct(int id);
		Task UpdateProduct(Product product);
		Task<IEnumerable<Product>> GetListProducts(string? search, string? sort, int? page, int? pageSize, string? include);
		Task<Product?> GetProductById(int id);
		Task<int> GetTotalProducts(string? search);
	}
}

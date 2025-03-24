using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN222.lab2.Repositories.Entities;
using PRN222.Lab2.Services.Services.ProductService;

namespace PRN222.Lab2.ProductManagementASPNETCoreRazorPage.Pages.Products
{
	[Authorize(Roles = "1")]
	public class IndexModel : PageModel
	{
		private readonly IProductService _productService;

		public IndexModel(IProductService productService)
		{
			_productService = productService;
		}

		public IList<Product> Product { get; set; } = default!;
		public string? Search { get; set; }
		public string? SortOrder { get; set; }
		public int CurrentPage { get; set; } = 1;
		public int TotalPages { get; set; }
		public int PageSize { get; set; } = 3;

		public async Task OnGetAsync([FromQuery] string? search, [FromQuery] string? sortOrder, [FromQuery] int? page)
		{
			Search = search;
			SortOrder = sortOrder;
			CurrentPage = page ?? 1;

			var products = await _productService.GetListProducts(search, sortOrder, CurrentPage, PageSize, "Category");

			// Lấy tổng số sản phẩm để tính số trang
			int totalItems = await _productService.GetTotalProducts(search);

			// Gán danh sách sản phẩm
			Product = products.ToList();
			TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
		}
	}
}

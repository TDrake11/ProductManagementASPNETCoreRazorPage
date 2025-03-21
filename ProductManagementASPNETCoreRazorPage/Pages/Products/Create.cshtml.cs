using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using PRN222.lab2.Repositories.Entities;
using PRN222.Lab2.Services.Services.CategoryService;
using PRN222.Lab2.Services.Services.ProductService;

namespace PRN222.Lab2.ProductManagementASPNETCoreRazorPage.Pages.Products
{
	[Authorize(Roles = "1")]
	public class CreateModel : PageModel
    {
		private readonly IProductService _productService;
		private readonly ICategoryService _categoryService;

		public CreateModel(IProductService productService, ICategoryService categoryService)
		{
			_productService = productService;
			_categoryService = categoryService;
		}

		public async Task<IActionResult> OnGet()
        {
            var listCategory = await _categoryService.GetListCategories();
			ViewData["CategoryId"] = new SelectList(listCategory, "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _productService.CreateProduct(Product);

            return RedirectToPage("./Index");
        }
    }
}

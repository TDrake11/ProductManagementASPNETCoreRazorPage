using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PRN222.lab2.Repositories.Entities;
using PRN222.Lab2.ProductManagementASPNETCoreRazorPage.Helps;
using PRN222.Lab2.Services.Services.CategoryService;
using PRN222.Lab2.Services.Services.ProductService;

namespace PRN222.Lab2.ProductManagementASPNETCoreRazorPage.Pages.Products
{
	[Authorize(Roles = "1")]
    public class EditModel : PageModel
    {
		private readonly IProductService _productService;
		private readonly ICategoryService _categoryService;

		public EditModel(IProductService productService, ICategoryService categoryService)
		{
			_productService = productService;
			_categoryService = categoryService;
		}

		[BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  await _productService.GetProductById((int) id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            var listCategory = await _categoryService.GetListCategories();
			ViewData["CategoryId"] = new SelectList(listCategory, "CategoryId", "CategoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _productService.UpdateProduct(Product); ;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return (_productService.GetProductById(id) == null) ? true : false;
        }
    }
}

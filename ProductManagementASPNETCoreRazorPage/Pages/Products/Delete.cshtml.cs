using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PRN222.lab2.Repositories.Entities;
using PRN222.Lab2.ProductManagementASPNETCoreRazorPage.Helps;
using PRN222.Lab2.Services.Services.ProductService;

namespace PRN222.Lab2.ProductManagementASPNETCoreRazorPage.Pages.Products
{
	[Authorize(Roles = "1")]
	public class DeleteModel : PageModel
    {
		private readonly IProductService _productService;
		private readonly IHubContext<SignalRServer> _hubContext;

		public DeleteModel(IProductService productService, IHubContext<SignalRServer> hubContext)
		{
			_productService = productService;
			_hubContext = hubContext;
		}

		[BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductById((int)id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductById((int)id);
            if (product != null)
            {
                Product = product;
                await _productService.DeleteProduct((int)id);
				await _hubContext.Clients.All.SendAsync("ProductDeleted", id); ;
			}

            return RedirectToPage("./Index");
        }
    }
}

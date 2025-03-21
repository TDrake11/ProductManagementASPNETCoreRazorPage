using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PRN222.lab2.Repositories.Data;
using PRN222.lab2.Repositories.Entities;
using PRN222.Lab2.ProductManagementASPNETCoreRazorPage.Helps;
using PRN222.Lab2.Services.Services.AccountService;
using PRN222.Lab2.Services.Services.CategoryService;
using PRN222.Lab2.Services.Services.ProductService;

namespace ProductManagementASPNETCoreRazorPage
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorPages();

			builder.Services.AddDbContext<MyStoreDbContext>();

			// Đăng ký Service
			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IProductService, ProductService>();
			builder.Services.AddScoped<ICategoryService, CategoryService>();
			builder.Services.AddScoped<IAccountService, AccountService>();

			builder.Services.AddSignalR();

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
				options.ExpireTimeSpan = TimeSpan.FromDays(7); // Set cookie expiration
				options.Cookie.HttpOnly = true; // Secure the cookie (HTTP only)
				options.Cookie.IsEssential = true; // Ensure cookie is created even without user consent
				options.SlidingExpiration = true; // Renew cookie on activity
				options.LoginPath = "/Account/Login"; // Redirect to login page on unauthorized access
				options.LogoutPath = "/Account/Logout";
				options.AccessDeniedPath = "/AccessDenied"; // Custom path for access denied
			});

			builder.Services.AddAuthorization();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.MapHub<SignalRServer>("/SignalRServer");

			app.UseHttpsRedirection();
			app.UseStaticFiles();


			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapRazorPages();

			app.Run();
		}
	}
}

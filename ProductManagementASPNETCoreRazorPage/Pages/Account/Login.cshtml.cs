using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN222.lab2.Repositories.Entities;
using PRN222.Lab2.Services.Services.AccountService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace PRN222.Lab2.ProductManagementASPNETCoreRazorPage.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;

        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public AccountMember AccountMember { get; set; } = default!;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // ✅ Check if the email exists in the database
            var user = await _accountService.GetAccountMember(AccountMember.EmailAddress);
            if (user == null)
            {
                ModelState.AddModelError("AccountMember.EmailAddress", "Email not found.");
                return Page();
            }

            // ✅ Check if the password is correct
            if (user.MemberPassword != AccountMember.MemberPassword)
            {
                ModelState.AddModelError("AccountMember.MemberPassword", "Invalid password.");
                return Page();
            }

            // ✅ Create user claims for successful login
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.EmailAddress),
			    new Claim(ClaimTypes.Name, user.FullName),// FullName as a custom claim
                new Claim(ClaimTypes.NameIdentifier, user.MemberId.ToString()),
                new Claim(ClaimTypes.Role, user.MemberRole.ToString()) // Optional: Add user role
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // ✅ Sign in the user
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true, // Keep the user signed in after closing the browser
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7) // Set cookie expiration
                });

            // ✅ Redirect to the homepage after successful login
            return RedirectToPage("/Products/Index");
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Account/Login");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RideMatch.Controllers
{
	public class SecurityController : Controller
	{
		public IActionResult Login()
		{
			return Challenge(new AuthenticationProperties { RedirectUri = "/" });
		}

		public async Task Logout()
		{
			await HttpContext.SignOutAsync(
				CookieAuthenticationDefaults.AuthenticationScheme);
			await HttpContext.SignOutAsync(
				OpenIdConnectDefaults.AuthenticationScheme);
		}
	}
}

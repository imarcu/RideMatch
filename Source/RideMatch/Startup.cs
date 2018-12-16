using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using RideMatch.Models;

namespace RideMatch
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});
			var azureSettingsSection = Configuration.GetSection("AzureSettings");
			var azureSettings = azureSettingsSection.Get<AzureSettings>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme =
					CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultSignInScheme =
					CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme =
					OpenIdConnectDefaults.AuthenticationScheme;
			}).AddOpenIdConnect(options =>
			{
				options.Authority = "https://login.microsoftonline.com/" +
				                    azureSettings.Tenant;
				options.ClientId = azureSettings.ClientId;
				options.ResponseType = OpenIdConnectResponseType.IdToken;
				options.CallbackPath = "/security/signin-callback";
				options.SignedOutRedirectUri = azureSettings.RootUrl;
			}).AddCookie();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseDeveloperExceptionPage();

			//if (env.IsDevelopment())
			//{
			//}
			//else
			//{
			//	app.UseExceptionHandler("/Home/Error");
			//}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}

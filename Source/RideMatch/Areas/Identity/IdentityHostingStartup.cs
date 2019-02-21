using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RideMatch.Areas.Identity.Data;
using RideMatch.Models;

[assembly: HostingStartup(typeof(RideMatch.Areas.Identity.IdentityHostingStartup))]
namespace RideMatch.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<RideMatchContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("RideMatchContextConnection")));

                services.AddDefaultIdentity<RideMatchUser>()
                    .AddEntityFrameworkStores<RideMatchContext>();
            });
        }
    }
}
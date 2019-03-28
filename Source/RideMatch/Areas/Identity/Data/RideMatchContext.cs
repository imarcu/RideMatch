using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using RideMatch.Models;


namespace RideMatch.Areas.Identity.Data
{
	public class RideMatchContext : IdentityDbContext<RideMatchUser>
    {
        public DbSet<Location> Locations;

        public DbSet<Campus> Campuses;

		public RideMatchContext(DbContextOptions<RideMatchContext> options)
			: base(options)
		{
            
		}

		protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Campus>().HasOne(x => x.Location);

			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
		}
	}
}

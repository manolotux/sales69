using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sales.Shared.Entidades;

namespace Sales.API.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
    {
	    public DbSet<Category> Categories { get; set; }
	    public DbSet<City> Cities { get; set; }
		public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();

			modelBuilder.Entity<State>().HasIndex(nameof(State.CountryId), nameof(State.Name)).IsUnique();
			modelBuilder.Entity<City>().HasIndex(nameof(City.StateId), nameof(City.Name)).IsUnique();
        }
    }
}

using Sales.Shared.Entidades;

namespace Sales.API.Data
{
    public class SeedDb(AppDbContext _context)
    {
        public async Task SeedAsync()
		{
			await _context.Database.EnsureCreatedAsync();
			await CheckCountriesAsync();
		}

		private async Task CheckCountriesAsync()
		{
			if (!_context.Countries.Any())
			{
				_context.Countries.Add(new Country { Name = "México" });
				_context.Countries.Add(new Country { Name = "Estados Unidos" });
				_context.Countries.Add(new Country { Name = "Panama" });
				await _context.SaveChangesAsync();
			}
		}
    }
}

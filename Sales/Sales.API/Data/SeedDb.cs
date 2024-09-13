using Sales.Shared.Entidades;

namespace Sales.API.Data
{
    public class SeedDb(AppDbContext _context)
    {
        public async Task SeedAsync()
		{
			await _context.Database.EnsureCreatedAsync();
			await CheckCountriesAsync();
			await CheckCategoriesAsync();
		}

		private async Task CheckCountriesAsync()
		{
			if (!_context.Countries.Any())
			{
				_context.Countries.Add(new Country
				{
					Name = "México",
					States = new List<State>
					{
						new()
						{
							Name = "Chiapas",
							Cities = new List<City>
							{
								new() { Name = "Tuxtla Gutierrez" },
								new() { Name = "Villa Flores" },
								new() { Name = "Chiapa de Corzo" },
								new() { Name = "San Cristobal de las Casas" },
							}
						},
						new()
						{
							Name = "Veracruz",
							Cities = new List<City>
							{
								new() { Name = "Coatzacoalcos" },
								new() { Name = "Agua Dulce" },
								new() { Name = "Mina" },
								new() { Name = "Jalapa" },
							}
						}
					}
				});
				_context.Countries.Add(new Country
				{
					Name = "Estados Unidos",
					States = new List<State>
					{
						new()
						{
							Name = "Florida",
							Cities = new List<City>
							{
								new() { Name = "Orlando" },
								new() { Name = "Miami" },
								new() { Name = "Tampa" },
								new() { Name = "Key West" },
							}
						},
						new()
						{
							Name = "Texas",
							Cities = new List<City>
							{
								new() { Name = "Houston" },
								new() { Name = "San Antonio" },
								new() { Name = "Dallas" },
								new() { Name = "Austin" },
							}
						}
					}
				});
				await _context.SaveChangesAsync();
			}
		}

		private async Task CheckCategoriesAsync()
		{
			if (!_context.Categories.Any())
			{
				_context.Categories.Add(new Category { Name = "Categoria 1" });
				_context.Categories.Add(new Category { Name = "Categoria 2" });
				_context.Categories.Add(new Category { Name = "Categoria 3" });
				await _context.SaveChangesAsync();
			}
		}
	}
}

using Sales.API.Servicios;
using Sales.Shared.Entidades;

namespace Sales.API.Data
{
    public class SeedDb(AppDbContext _context, IApiService _apiService)
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
				var listPaises = await _apiService.getListCountriesAsync();

				if (listPaises.Countries == null || listPaises.Countries.Count == 0) return;
				
				foreach (var pais in listPaises.Countries.Where(z=> z.Iso2 is "MX" or "US" or "CO" or "CR"))
				{
					var newCountry = new Country
					{
						Name = pais.Name!,
						States = new List<State>()
					};
					
					var listEstados = await _apiService.getListStatesAsync(pais.Iso2!);

					if (listEstados.States == null || listEstados.States.Count == 0) continue;

					foreach (var estado in listEstados.States)
					{
						var newState = new State
						{
							Name = estado.Name!,
							Cities = new List<City>()
						};
						
						var listCiudades = await _apiService.getListCitiesAsync(pais.Iso2!, estado.Iso2!);
						
						if (listCiudades.Cities == null || listCiudades.Cities.Count == 0) continue;
						
						foreach (var newCity in listCiudades.Cities.Select(ciudad => new City { Name = ciudad.Name! }))
						{
							newState.Cities.Add(newCity);
						}

						newCountry.States.Add(newState);
					}
					_context.Countries.Add(newCountry);
				}
				await _context.SaveChangesAsync();
			}
		}
	}
}

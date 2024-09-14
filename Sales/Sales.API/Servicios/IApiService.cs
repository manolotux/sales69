using Sales.Shared.Responses;

namespace Sales.API.Servicios
{
	public interface IApiService
	{
		Task<CountryResponse> getListCountriesAsync();
		Task<StatesResponse> getListStatesAsync(string pais);
		Task<CitiesResponse> getListCitiesAsync(string pais, string estado);

	}
}

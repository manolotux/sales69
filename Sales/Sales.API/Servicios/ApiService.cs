using Sales.Shared.Responses;
using Newtonsoft.Json;

namespace Sales.API.Servicios
{
	public class ApiService(IConfiguration configuration) : IApiService
	{
		private readonly string _tokenName = configuration["CountriesAPI:tokenName"]!;
		private readonly string _urlBase = configuration["CountriesAPI:urlBase"]!;
		private readonly string _tokenValue = configuration["CountriesAPI:tokenValue"]!;

		public async Task<CountryResponse> getListCountriesAsync()
		{
			var result = await ResponseBase("https://api.countrystatecity.in/v1/countries");

			if (result.response == null)
				return new CountryResponse(false, result.res, null);

			var list = JsonConvert.DeserializeObject<List<CountryDTO>>(result.res);
			return new CountryResponse(true, string.Empty, list);
		}

		public async Task<StatesResponse> getListStatesAsync(string pais)
		{
			var result = await ResponseBase($"https://api.countrystatecity.in/v1/countries/{pais}/states");

			if (result.response == null)
				return new StatesResponse(false, result.res, null);

			var list = JsonConvert.DeserializeObject<List<StateDTO>>(result.res);
			return new StatesResponse(true, string.Empty, list);
		}

		public async Task<CitiesResponse> getListCitiesAsync(string pais, string estado)
		{
			var result = await ResponseBase($"https://api.countrystatecity.in/v1/countries/{pais}/states/{estado}/cities");

			if (result.response == null)
				return new CitiesResponse(false, result.res, null);

			var list = JsonConvert.DeserializeObject<List<CityDTO>>(result.res);
			return new CitiesResponse(true, string.Empty, list);
		}

		private async Task<(HttpResponseMessage? response, string res)> ResponseBase(string url)
		{
			try
			{
				var client = new HttpClient{BaseAddress = new Uri(_urlBase) };
				client.DefaultRequestHeaders.Add(_tokenName, _tokenValue);
				var response = await client.GetAsync(url);
				var result = await response.Content.ReadAsStringAsync();

				if (!response.IsSuccessStatusCode)
					throw new ApplicationException(result);

				return (response, result);
			}
			catch (Exception ex)
			{
				return (null, ex.Message);
			}
		}
	}
}

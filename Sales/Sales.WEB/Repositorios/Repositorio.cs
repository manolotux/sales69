using System.Text.Json;

namespace Sales.WEB.Repositorios
{
    public class Repositorio(HttpClient _httpClient) : IRepositorio
    {
        private JsonSerializerOptions _jsonSerializerOptions => new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return new HttpResponseWrapper<T>(default, true, response);

            var result = await UnserializeAnswer<T>(response, _jsonSerializerOptions);
            return new HttpResponseWrapper<T>(result, false, response);
        }   

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T model)
        {
            var message = JsonSerializer.Serialize(model);
            var request = new StringContent(message, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, request);
            return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T model)
        {
            var message = JsonSerializer.Serialize(model);
            var request = new StringContent(message, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, request);

            if (!response.IsSuccessStatusCode)
                return new HttpResponseWrapper<TResponse>(default, true, response);

            var result = await UnserializeAnswer<TResponse>(response, _jsonSerializerOptions);
            return new HttpResponseWrapper<TResponse>(result, !response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
	        var response = await _httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T model)
        {
			var message = JsonSerializer.Serialize(model);
			var request = new StringContent(message, System.Text.Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync(url, request);
			return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
		}

        public async Task<HttpResponseWrapper<TResponse>> Püt<T, TResponse>(string url, T model)
        {
			var message = JsonSerializer.Serialize(model);
			var request = new StringContent(message, System.Text.Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync(url, request);

			if (!response.IsSuccessStatusCode)
				return new HttpResponseWrapper<TResponse>(default, true, response);

			var result = await UnserializeAnswer<TResponse>(response, _jsonSerializerOptions);
			return new HttpResponseWrapper<TResponse>(result, !response.IsSuccessStatusCode, response);
		}

        private async Task<T> UnserializeAnswer<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            var responseText = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseText, jsonSerializerOptions)!;
        }
    }
}

using System.Text.Json.Serialization;

namespace Sales.Shared.Responses
{
	public record CitiesResponse(bool Success, string Message, List<CityDTO>? Cities);

	public class CityDTO
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }
		[JsonPropertyName("name")]
		public string? Name { get; set; } = null!;
	}
}

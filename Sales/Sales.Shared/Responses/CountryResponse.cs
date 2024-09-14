using System.Text.Json.Serialization;

namespace Sales.Shared.Responses
{
	public record CountryResponse(bool Success, string Message, List<CountryDTO>? Countries);

	public class CountryDTO
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }
		[JsonPropertyName("name")]
		public string? Name { get; set; } = null!;
		[JsonPropertyName("iso2")]
		public string? Iso2 { get; set; } = null!;
	}
}

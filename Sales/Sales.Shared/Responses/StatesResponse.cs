using System.Text.Json.Serialization;

namespace Sales.Shared.Responses
{
	public record StatesResponse(bool Success, string Message, List<StateDTO>? States);

	public class StateDTO
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }
		[JsonPropertyName("name")]
		public string? Name { get; set; } = null!;
		[JsonPropertyName("iso2")]
		public string? Iso2 { get; set; } = null!;
	}
}

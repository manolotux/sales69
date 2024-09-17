
namespace Sales.Shared.DTO
{
	public class PaginationDTO
	{
		public int Id { get; set; }
		public int Page { get; set; } = 1;
		public int Records { get; set; } = 10;
		public string? Filter { get; set; }
	}
}

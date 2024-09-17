using Sales.Shared.DTO;

namespace Sales.API._Helpers
{
	public static class QueryableExtensions
	{
		public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO pagination)
		{
			return queryable
				.Skip((pagination.Page - 1) * pagination.Records)
				.Take(pagination.Records);
		}
	}
}

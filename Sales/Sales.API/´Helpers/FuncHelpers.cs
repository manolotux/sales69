namespace Sales.API._Helpers
{
	public static class FuncHelpers
	{
		public static double PagesCount(this double totalRecords, double recordsPerPage)
		{
			return Math.Ceiling(totalRecords / recordsPerPage);
		}
	}
}

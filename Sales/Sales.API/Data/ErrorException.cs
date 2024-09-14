using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Sales.API.Data
{
	public static class ErrorException
	{
		public static string GetExceptionMessage(this DbUpdateException ex)
		{
			var message = ex.Message;
			var innerException = ex.InnerException;
			if (innerException is MySqlException dbException)
			{
				if (dbException.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
				{
					return "El registro ya existe, favor de verificar";
				}
			}
			return message;
		}
	}
}

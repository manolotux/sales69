using Microsoft.AspNetCore.Identity;
using Sales.Shared.Entidades;
using Sales.Shared.Shared;

namespace Sales.API._Helpers
{
	public interface IUserHelper
	{
		Task<User?> GetUserAsync(string email);
		Task<IdentityResult> AddUserAync(User user, string password);
		Task CheckRoleAsync(UserType rol);
		Task AddUserToRoleAsync(User user, UserType rol);
		Task<bool> IsUserInroleAsync(User user, UserType rol);
	}
}

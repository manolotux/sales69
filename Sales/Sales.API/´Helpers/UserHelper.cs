using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entidades;
using Sales.Shared.Shared;

namespace Sales.API._Helpers
{
	public class UserHelper(AppDbContext _context, UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager) : IUserHelper
	{
		public async Task<User?> GetUserAsync(string email) => await _context.Users
			.Include(x => x.City)
			.ThenInclude(s => s!.State)
			.ThenInclude(c => c!.Country)
			.FirstOrDefaultAsync(x => x.Email == email);

		public async Task<IdentityResult> AddUserAync(User user, string password) => await _userManager.CreateAsync(user, password);
		
		public async Task CheckRoleAsync(UserType rol)
		{
			var roleExists = await _roleManager.RoleExistsAsync(rol.ToString());
			if (!roleExists) 
				await _roleManager.CreateAsync(new IdentityRole { Name = rol.ToString() });
		}

		public async Task AddUserToRoleAsync(User user, UserType rol) => await _userManager.AddToRoleAsync(user, rol.ToString());
		
		public async Task<bool> IsUserInroleAsync(User user, UserType rol) => await _userManager.IsInRoleAsync(user, rol.ToString());
	}
}

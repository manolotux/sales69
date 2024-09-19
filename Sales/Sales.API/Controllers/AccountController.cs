using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sales.API.Servicios;
using Sales.Shared.DTO;
using Sales.Shared.Entidades;

namespace Sales.API.Controllers
{
	[ApiController]
	[Route("api/accounts")]
	public class AccountController(IUserHelper _userHelper, IConfiguration _configuration) : Controller
	{
		[HttpPost("create")]
		public async Task<IActionResult> Create([FromBody] UserDTO model)
        {
            User user = model;
			var result = await _userHelper.AddUserAync(user, model.Password);
			if (!result.Succeeded)
				return BadRequest(result.Errors.FirstOrDefault()!.Description);

			await _userHelper.AddUserToRoleAsync(user, user.Usertype);
            return Ok(GeneraToken(user));
		}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var result = await _userHelper.LoginAsync(model);
            if (!result.Succeeded)
                return BadRequest("Email o contraseña incorrectos");

            var user = await _userHelper.GetUserAsync(model.Email);
            return Ok(GeneraToken(user));
        }

        private TokenDTO? GeneraToken(User? user)
        {
            if (user == null) return null;

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Email!),
                new(ClaimTypes.Role, user.Usertype.ToString()),
                new("Photo", user.Photo),
                new("CityId", user.CityId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(5);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );  

            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
            
        }

    }
}


using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Sales.Shared.Shared;

namespace Sales.Shared.Entidades
{
	public class User : IdentityUser
	{
		[Display(Name = "Nombre")]
		[Required(ErrorMessage = "El campo {0} es obligatorio")]
		public string Name { get; set; } = null!;

		[Display(Name = "Foto")]
		public string Photo { get; set; } = string.Empty;

		[Display(Name = "Tipo de Usuario")]
		public UserType Usertype { get; set; }

		public City? City { get; set; }

		[Display(Name = "Ciudad")]
		[Range(1, int.MaxValue, ErrorMessage = "El campo {0} es obligatorio")]
		public int CityId { get; set; }	
	}
}

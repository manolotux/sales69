using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entidades
{
    public class State
    {
		public int Id { get; set; }
		
		public int CountryId { get; set; }

		[Display(Name = "Estado")]
		[Required(ErrorMessage = "El campo {0} es obligatorio")]
		[MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
		public string Name { get; set; } = null!;

		public Country? Country { get; set; }

		public ICollection<City>? Cities { get; set; }
		
		public int CitiesCount => Cities?.Count ?? 0;
	}
}

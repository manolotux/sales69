using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entidades
{
    public class Country
    {
        public int Id { get; set; }

        [Display(Name = "Pais")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Name { get; set; } = null!;

        public ICollection<State>? States { get; set; }

        public int StatesCount => States?.Count ?? 0;
    }
}

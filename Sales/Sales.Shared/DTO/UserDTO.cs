using System.ComponentModel.DataAnnotations;
using Sales.Shared.Entidades;

namespace Sales.Shared.DTO
{
    public class UserDTO : User
    {
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no son iguales.")]
        [Display(Name = "Confirmación de la contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
        public string PasswordConfirm { get; set; } = null!;
    }
}

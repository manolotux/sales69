using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email es requerido")]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; } = null!;

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Contraseña es requerida")]
        public string Password { get; set; } = null!;

    }
}

using System.ComponentModel.DataAnnotations;

namespace FarmaTech.Shared.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "El PIN es obligatorio.")]
        [RegularExpression(@"^\d{4,6}$", ErrorMessage = "El PIN debe contener entre 4 y 6 digitos.")]
        public string Pin { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;

namespace FarmaTech.Shared.DTO
{
    public class EmpleadaDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido no puede superar los 100 caracteres.")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El usuario es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El usuario debe tener entre 3 y 50 caracteres.")]
        [RegularExpression(@"^[A-Za-z0-9._-]+$", ErrorMessage = "El usuario solo puede contener letras, numeros, puntos, guiones y guiones bajos.")]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "El PIN es obligatorio.")]
        [RegularExpression(@"^\d{4,6}$", ErrorMessage = "El PIN debe contener entre 4 y 6 digitos.")]
        public string Pin { get; set; } = string.Empty;
    }
}

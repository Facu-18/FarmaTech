using System.ComponentModel.DataAnnotations;
using FarmaTech.BD.Datos;

namespace FarmaTech.BD.Datos.Entity
{
    public class Producto : EntityBase
    {
        [Required(ErrorMessage = "El codigo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El codigo no puede superar los 50 caracteres.")]
        [RegularExpression(@"^[A-Za-z0-9][A-Za-z0-9._-]*$", ErrorMessage = "El codigo solo puede contener letras, numeros, puntos, guiones y guiones bajos.")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripcion es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripcion no puede superar los 500 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La categoria es obligatoria.")]
        [StringLength(100, ErrorMessage = "La categoria no puede superar los 100 caracteres.")]
        public string Categoria { get; set; } = string.Empty;

        [Range(typeof(decimal), "0.01", "9999999999999999.99", ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [StringLength(30, ErrorMessage = "El estado no puede superar los 30 caracteres.")]
        public string Estado { get; set; } = string.Empty;
    }
}

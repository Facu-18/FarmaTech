using System.ComponentModel.DataAnnotations;
using FarmaTech.BD.Datos;

namespace FarmaTech.BD.Datos.Entity
{
    public class Drogueria : EntityBase
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El canal de contacto es obligatorio.")]
        [StringLength(30, ErrorMessage = "El canal de contacto no puede superar los 30 caracteres.")]
        public string CanalContacto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El contacto es obligatorio.")]
        [StringLength(150, ErrorMessage = "El contacto no puede superar los 150 caracteres.")]
        public string Contacto { get; set; } = string.Empty;
    }
}

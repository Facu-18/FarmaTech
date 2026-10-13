using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FarmaTech.BD.Datos;

namespace FarmaTech.BD.Datos.Entity
{
    public class NotaPedido : EntityBase
    {
        [ValidDate]
        public DateTime Fecha { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [StringLength(30, ErrorMessage = "El estado no puede superar los 30 caracteres.")]
        public string Estado { get; set; } = string.Empty;

        [ForeignKey(nameof(Producto))]
        public int? IdProducto { get; set; }

        public Producto? Producto { get; set; }

        [ForeignKey(nameof(Drogueria))]
        public int? IdDrogueria { get; set; }

        public Drogueria? Drogueria { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una empleada valida.")]
        [ForeignKey(nameof(Empleada))]
        public int IdEmpleada { get; set; }

        public Empleada Empleada { get; set; } = null!;
    }
}

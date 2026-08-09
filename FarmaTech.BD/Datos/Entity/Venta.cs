using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FarmaTech.BD.Datos;

namespace FarmaTech.BD.Datos.Entity
{
    public class Venta : EntityBase
    {
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una empleada valida.")]
        [ForeignKey(nameof(Empleada))]
        public int IdEmpleada { get; set; }

        public Empleada Empleada { get; set; } = null!;

        [ValidDate]
        public DateTime Fecha { get; set; }

        [Range(typeof(decimal), "0.01", "9999999999999999.99", ErrorMessage = "El total debe ser mayor que cero.")]
        public decimal Total { get; set; }
    }
}

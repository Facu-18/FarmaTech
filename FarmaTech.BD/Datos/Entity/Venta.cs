using System;
using System.Collections.Generic;
using FarmaTech.BD.Datos;

namespace FarmaTech.BD.Datos.Entity
{
    public class Venta : EntityBase
    {
        public int IdEmpleada { get; set; }
        public Empleada Empleada { get; set; } = null!;

        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        public ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>();
    }
}

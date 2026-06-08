using System.Collections.Generic;
using FarmaTech.BD.Datos;

namespace FarmaTech.BD.Datos.Entity
{
    public class DetalleVenta : EntityBase
    {
        public int IdVenta { get; set; }
        public Venta Venta { get; set; } = null!;

        public int IdProducto { get; set; }
        public Producto Producto { get; set; } = null!;

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}

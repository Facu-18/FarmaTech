using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FarmaTech.BD.Datos;

namespace FarmaTech.BD.Datos.Entity
{
    public class DetalleVenta : EntityBase
    {
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una venta valida.")]
        [ForeignKey(nameof(Venta))]
        public int IdVenta { get; set; }

        public Venta Venta { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un producto valido.")]
        [ForeignKey(nameof(Producto))]
        public int IdProducto { get; set; }

        public Producto Producto { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
        public int Cantidad { get; set; }

        [Range(typeof(decimal), "0.01", "9999999999999999.99", ErrorMessage = "El precio unitario debe ser mayor que cero.")]
        public decimal PrecioUnitario { get; set; }

        [Range(typeof(decimal), "0.01", "9999999999999999.99", ErrorMessage = "El subtotal debe ser mayor que cero.")]
        public decimal Subtotal { get; set; }
    }
}

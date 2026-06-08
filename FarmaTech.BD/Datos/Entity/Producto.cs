using System.Collections.Generic;
using FarmaTech.BD.Datos;

namespace FarmaTech.BD.Datos.Entity
{
    public class Producto : EntityBase
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string Estado { get; set; } = string.Empty;

        public ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>();
        public ICollection<NotaPedido> NotasPedido { get; set; } = new List<NotaPedido>();
        public ICollection<IngresoMercaderia> IngresosMercaderia { get; set; } = new List<IngresoMercaderia>();
    }
}

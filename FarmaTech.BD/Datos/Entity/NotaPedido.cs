using System;
using FarmaTech.BD.Datos;

namespace FarmaTech.BD.Datos.Entity
{
    public class NotaPedido : EntityBase
    {
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; } = string.Empty;

        public int IdProducto { get; set; }
        public Producto Producto { get; set; } = null!;

        public int IdDrogueria { get; set; }
        public Drogueria Drogueria { get; set; } = null!;

        public int IdEmpleada { get; set; }
        public Empleada Empleada { get; set; } = null!;
    }
}

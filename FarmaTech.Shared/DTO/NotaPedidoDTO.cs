namespace FarmaTech.Shared.DTO
{
    public class NotaPedidoDTO
    {
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int? IdProducto { get; set; }
        public int? IdDrogueria { get; set; }
        public int IdEmpleada { get; set; }
    }
}

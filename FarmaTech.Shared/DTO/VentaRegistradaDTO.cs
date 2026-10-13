namespace FarmaTech.Shared.DTO
{
    public class VentaRegistradaDTO
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVentaRegistradaDTO> Detalles { get; set; } = new();
    }
}

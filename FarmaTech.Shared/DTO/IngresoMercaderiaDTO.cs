namespace FarmaTech.Shared.DTO
{
    public class IngresoMercaderiaDTO
    {
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public int IdProducto { get; set; }
        public int IdDrogueria { get; set; }
        public int IdEmpleada { get; set; }
    }
}

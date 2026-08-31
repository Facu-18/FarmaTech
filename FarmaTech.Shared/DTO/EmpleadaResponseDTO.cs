namespace FarmaTech.Shared.DTO
{
    public class EmpleadaResponseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}

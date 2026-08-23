namespace FarmaTech.Shared.DTO
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expira { get; set; }
        public int Id { get; set; }
        public string Rol { get; set; } = string.Empty;
    }
}

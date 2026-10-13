using System.ComponentModel.DataAnnotations;

namespace FarmaTech.Shared.DTO
{
    public class RegistrarVentaDTO
    {
        [MinLength(1, ErrorMessage = "La venta debe tener al menos un producto.")]
        public List<ItemVentaDTO> Items { get; set; } = new();
    }
}

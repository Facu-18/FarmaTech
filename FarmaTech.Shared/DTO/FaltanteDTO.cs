using System.ComponentModel.DataAnnotations;

namespace FarmaTech.Shared.DTO
{
    public class FaltanteDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un producto valido.")]
        public int IdProducto { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
        public int Cantidad { get; set; }

        public int? IdDrogueria { get; set; }
    }
}

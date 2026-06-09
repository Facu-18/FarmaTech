using System.Collections.Generic;
using FarmaTech.BD.Datos;

namespace FarmaTech.BD.Datos.Entity
{
    public class Drogueria : EntityBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string CanalContacto { get; set; } = string.Empty;
        public string Contacto { get; set; } = string.Empty;
    }
}

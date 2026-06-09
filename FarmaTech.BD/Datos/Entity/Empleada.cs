using System.Collections.Generic;
using FarmaTech.BD.Datos;

namespace FarmaTech.BD.Datos.Entity
{
    public class Empleada : EntityBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Pin { get; set; } = string.Empty;
    }
}

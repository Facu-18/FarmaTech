using System.ComponentModel.DataAnnotations;

namespace FarmaTech.BD.Datos
{
    public sealed class ValidDateAttribute : ValidationAttribute
    {
        public ValidDateAttribute()
        {
            ErrorMessage = "La fecha es obligatoria.";
        }

        public override bool IsValid(object? value)
        {
            return value is DateTime date && date != default;
        }
    }
}

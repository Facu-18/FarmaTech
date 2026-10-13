using System.Globalization;

namespace FarmaTech.Cliente
{
    public static class Formato
    {
        // The ICU data bundled with WebAssembly lacks the es-AR currency symbol, so it is set explicitly.
        private static readonly CultureInfo Cultura = CrearCultura();

        public static string Moneda(decimal valor) => valor.ToString("C", Cultura);

        private static CultureInfo CrearCultura()
        {
            var cultura = (CultureInfo)CultureInfo.GetCultureInfo("es-AR").Clone();
            cultura.NumberFormat.CurrencySymbol = "$";
            cultura.NumberFormat.CurrencyPositivePattern = 2; // $ n
            cultura.NumberFormat.CurrencyNegativePattern = 9; // -$ n
            return cultura;
        }
    }
}

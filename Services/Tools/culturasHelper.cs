using System.Globalization;

namespace Services.Tools
{
    public static class culturasHelper
    {
        public static CultureInfo[] CulturasSoportadas = new[] 
        {
            new CultureInfo("en-US"),
            new CultureInfo("es-AR"),
            new CultureInfo("en"),
            new CultureInfo("es")
        };
    }
}

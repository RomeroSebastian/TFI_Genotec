using System;
using System.Collections.Generic;

namespace Domain
{
    public class Tratamiento
    {
        public int IdTratamiento { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Indicaciones { get; set; }
        public DateTime Fecha { get; set; }
        public List<EnsayoClinico> TratamientoEnsayos { get; set; }
    }
}

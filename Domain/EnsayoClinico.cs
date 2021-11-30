using System;
using System.Collections.Generic;

namespace Domain
{
    public class EnsayoClinico
    {
        public int IdEnsayoClinico { get; set; }
        public string CodigoSISA { get; set; }
        public string Titulo { get; set; }
        public string CondicionSalud { get; set; }
        public string Categoria { get; set; }
        public string Tipo { get; set; }
        public string Fase { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Estado { get; set; }
        public List<PacienteEnsayo> PacienteEnsayo { get; set; }
    }
}

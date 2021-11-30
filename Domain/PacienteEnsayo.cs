using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PacienteEnsayo
    {
        public int IdPaciente { get; set; }
        public int IdEnsayoClinico { get; set; }
        public Paciente paciente { get; set; }
        public EnsayoClinico ensayoClinico { get; set; }
    }
}

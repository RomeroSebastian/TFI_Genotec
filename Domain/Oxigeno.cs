using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Oxigeno
    {
        public int  IdOxigeno { get; set; }
        public decimal Valor { get; set; }
        public string Unidad { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; }
    }
}

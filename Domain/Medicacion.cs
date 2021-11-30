using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Medicacion
    {
        public int IdMedicacion { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }
        public string Unidad { get; set; }
        public DateTime Fecha { get; set; }
        public Paciente Paciente { get; set; }
    }
}

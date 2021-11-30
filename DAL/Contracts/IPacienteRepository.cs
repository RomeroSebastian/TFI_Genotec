using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IPacienteRepository<Paciente>
    {
        void Add(Paciente paciente);
        void Update(Paciente paciente);
        IEnumerable<Paciente> GetAll(string filterExpression);
        Paciente GetOne(int idPaciente);
        IEnumerable<Paciente> GetAll();
    }
}

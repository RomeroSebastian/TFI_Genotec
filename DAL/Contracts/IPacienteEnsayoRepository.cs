using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IPacienteEnsayoRepository<PacienteEnsayo>
    {
        void Add(PacienteEnsayo pacienteEnsayo);
        void Update(PacienteEnsayo pacienteEnsayo);
        IEnumerable<PacienteEnsayo> GetAll(string filterExpression);
        PacienteEnsayo GetOne(int idPaciente, int idEnsayoClinico);
        IEnumerable<PacienteEnsayo> GetAll();
    }
}

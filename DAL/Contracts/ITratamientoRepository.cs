using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface ITratamientoRepository<Tratamiento>
    {
        void Add(Tratamiento tratamiento);
        void Update(Tratamiento tratamiento);
        IEnumerable<Tratamiento> GetAll(string filterExpression);
        IEnumerable<Tratamiento> GetAll();
        Tratamiento GetOne(int idTratamiento);
    }
}

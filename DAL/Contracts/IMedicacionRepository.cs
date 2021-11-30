using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IMedicacionRepository<Medicacion>
    {
        void Add(Medicacion medicacion);
        void Update(Medicacion medicacion);
        IEnumerable<Medicacion> GetAll(string filterExpression);
        IEnumerable<Medicacion> GetAll();
        Medicacion GetOne(int idMedicacion);
    }
}

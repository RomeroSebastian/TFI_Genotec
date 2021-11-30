using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IEnsayoClinicoRepository<EnsayoClinico>
    {
        void Add(EnsayoClinico ensayoClinico);
        void Update(EnsayoClinico ensayoClinico);
        IEnumerable<EnsayoClinico> GetAll(string? filterExpression);
        EnsayoClinico GetOne(int idEnsayoClinico);
        IEnumerable<EnsayoClinico> GetAll();
    }
}

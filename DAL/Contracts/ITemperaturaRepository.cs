using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface ITemperaturaRepository<Temperatura>
    {
        void Add(Temperatura temperatura);
        void Update(Temperatura temperatura);
        IEnumerable<Temperatura> GetAll(string filterExpression);
        IEnumerable<Temperatura> GetAll();
        Temperatura GetOne(int idTemperatura);
    }
}

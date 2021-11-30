using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Contracts
{
    public interface ICardioRepository<Cardio>
    {
        void Add(Cardio cardio);
        void Update(Cardio cardio);
        IEnumerable<Cardio> GetAll(string filterExpression);
        IEnumerable<Cardio> GetAll();
        Cardio GetOne(int idCardio);
    }
}

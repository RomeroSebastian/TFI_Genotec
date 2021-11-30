using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IOxigenoRepository<Oxigeno>
    {
        void Add(Oxigeno oxigeno);
        void Update(Oxigeno oxigeno);
        IEnumerable<Oxigeno> GetAll(string filterExpression);
        IEnumerable<Oxigeno> GetAll();
        Oxigeno GetOne(int idOxigeno);
        void Delete(Oxigeno oxigeno);
        //Task Add(Oxigeno oxigeno);
    }
}

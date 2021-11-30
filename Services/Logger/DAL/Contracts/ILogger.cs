using Services.Logger.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Logger.DAL.Contracts
{
    /// <summary>
    /// Interfaz para la bitácora
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILogger<T> where T : class, new()
    {
        void Add(T obj);

        IEnumerable<T> GetAll();

    }
}

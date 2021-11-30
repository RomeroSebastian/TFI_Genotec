using DAL.Contracts;
using Domain;
using System;

namespace DAL.Repositories.Sql.Adapters
{
    /// <summary>
    /// Clase para adaptar valores a los campos de un objeto Temperatura
    /// </summary>
    public sealed class TemperaturaAdapter
    {
        #region Singleton
        private readonly static TemperaturaAdapter _instance = new TemperaturaAdapter();

        public static TemperaturaAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private TemperaturaAdapter()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public Temperatura Adapt(object[] values)
        {

            return new Temperatura()
            {
                IdTemperatura = Convert.ToInt16(values[0]),
                Valor = Convert.ToDecimal(values[1]),
                Unidad = values[2].ToString(),
                Fecha = Convert.ToDateTime(values[3]),
                IdPaciente = Convert.ToInt16(values[4])
            };
        }
    }

}

using DAL.Contracts;
using Domain;
using System;

namespace DAL.Repositories.Sql.Adapters
{
    /// <summary>
    /// Clase para adaptar valores a los campos de un objeto Cardio
    /// </summary>
    public sealed class CardioAdapter
    {
        #region Singleton
        private readonly static CardioAdapter _instance = new CardioAdapter();

        public static CardioAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private CardioAdapter()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public Cardio Adapt(object[] values)
        {

            return new Cardio()
            {
                IdCardio = Convert.ToInt16(values[0]),
                Valor = Convert.ToDecimal(values[1]),
                Unidad = values[2].ToString(),
                Fecha = Convert.ToDateTime(values[3]),
                IdPaciente = Convert.ToInt16(values[4])
            };
        }
    }
}

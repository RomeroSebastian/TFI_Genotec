using DAL.Contracts;
using Domain;
using System;

namespace DAL.Repositories.Sql.Adapters
{
    public sealed class OxigenoAdapter
    {
        #region Singleton
        private readonly static OxigenoAdapter _instance = new OxigenoAdapter();

        public static OxigenoAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private OxigenoAdapter()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public Oxigeno Adapt(object[] values)
        {

            return new Oxigeno()
            {
                IdOxigeno = Convert.ToInt16(values[0]),
                Valor = Convert.ToDecimal(values[1]),
                Unidad = values[2].ToString(),
                Fecha = Convert.ToDateTime(values[3]),
                IdPaciente = Convert.ToInt16(values[4])
            };
        }
    }

}

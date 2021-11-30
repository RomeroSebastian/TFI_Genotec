using DAL.Contracts;
using Domain;
using System;

namespace DAL.Repositories.Sql.Adapters
{
    /// <summary>
    /// Clase para adaptar valores a los campos de un objeto PacienteEnsayo
    /// </summary>
    public sealed class PacienteEnsayoAdapter : IEntityAdapter<PacienteEnsayo>
    {
        #region Singleton
        private readonly static PacienteEnsayoAdapter _instance = new PacienteEnsayoAdapter();

        public static PacienteEnsayoAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private PacienteEnsayoAdapter()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public PacienteEnsayo Adapt(object[] values)
        {
            return new PacienteEnsayo()
            {
                IdPaciente = Convert.ToInt16(values[0]),
                IdEnsayoClinico = Convert.ToInt16(values[1])
            };
        }
    }
}

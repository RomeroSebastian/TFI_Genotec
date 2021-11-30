using DAL.Contracts;
using Domain;
using System;

namespace DAL.Repositories.Sql.Adapters
{
    /// <summary>
    /// Clase para adaptar valores a los campos de un objeto Tratamiento
    /// </summary>
    public sealed class TratamientoAdapter : IEntityAdapter<Tratamiento>
    {
        #region Singleton
        private readonly static TratamientoAdapter _instance = new TratamientoAdapter();

        public static TratamientoAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private TratamientoAdapter()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public Tratamiento Adapt(object[] values)
        {

            return new Tratamiento()
            {
                IdTratamiento = Convert.ToInt16(values[0]),
                Nombre = values[1].ToString(),
                Descripcion = values[2].ToString(),
                Indicaciones = values[3].ToString(),
                Fecha = Convert.ToDateTime(values[4])
            };
        }
    }

}

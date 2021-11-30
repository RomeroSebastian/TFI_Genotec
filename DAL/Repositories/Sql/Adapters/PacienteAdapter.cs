using DAL.Contracts;
using Domain;
using System;

namespace DAL.Repositories.Sql.Adapters
{
    /// <summary>
    /// Clase para adaptar valores a los campos de un objeto Paciente
    /// </summary>
    public sealed class PacienteAdapter : IEntityAdapter<Paciente>
    {
        #region Singleton
        private readonly static PacienteAdapter _instance = new PacienteAdapter();

        public static PacienteAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private PacienteAdapter()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public Paciente Adapt(object[] values)
        {

            return new Paciente()
            {
                IdPaciente = Convert.ToInt16(values[0]),
                Nombre = values[1].ToString(),
                Apellido = values[2].ToString(),
                Documento = values[3].ToString(),
                Domicilio = values[4].ToString(),
                Telefono = values[5].ToString(),
                Email = values[6].ToString(),
                Genero = values[7].ToString(),
                FechaNacimiento = Convert.ToDateTime(values[8])
            };
        }
    }
}

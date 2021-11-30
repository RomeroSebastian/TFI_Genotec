using DAL.Factory;
using Domain;
using Services.Exceptions.Domain;
using Services.Facade;
using System;
using System.Collections.Generic;

namespace BLL
{
    public sealed class TemperaturaBLL
    {
        #region Singleton
        private readonly static TemperaturaBLL _instance = new TemperaturaBLL();

        public static TemperaturaBLL Current
        {
            get
            {
                return _instance;
            }
        }

        private TemperaturaBLL()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        /// <summary>
        /// Crear un registro de Temperatura
        /// </summary>
        /// <param name="obj">Recibe un objeto Temperatura</param>
        public void Add(Temperatura obj)
        {
            try
            {
                Factory.Current.GetRepositoryTemperatura().Add(obj);

            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new BLLException(ex));
            }
        }

        /// <summary>
        /// Actualiza un registro de Temperatura
        /// </summary>
        /// <param name="obj">Recibe un objeto Tratamiento</param>
        public void Update(Temperatura obj)
        {
            try
            {
                Factory.Current.GetRepositoryTemperatura().Update(obj);
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
            }
        }

        /// <summary>
        /// Obtener todos los registros de Temperatura para un Paciente específico
        /// </summary>
        /// <param name="paciente">Recibe un objeto Paciente</param>
        /// <returns>Retorna una lista de objetos Tratamiento</returns>
        public IEnumerable<Temperatura> GetAll(Paciente paciente)
        {
            try
            {
                string filterExpression = "IdPaciente = " + paciente.IdPaciente;

                return Factory.Current.GetRepositoryTemperatura().GetAll(filterExpression);
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }

        /// <summary>
        /// Obtener un regsitro de Temperatura
        /// </summary>
        /// <param name="Id">Recibe el Id del registro de Temperatura</param>
        /// <returns>Retorna una objeto Tratamiento</returns>
        public Temperatura GetOne(int id)
        {
            try
            {
                Temperatura temperatura = new Temperatura();
                temperatura = Factory.Current.GetRepositoryTemperatura().GetOne(id);

                // Paciente asociado al registro de temperatura
                temperatura.Paciente = PacienteBLL.Current.GetOne(temperatura.IdPaciente);

                return temperatura;
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }
    }

}

using DAL.Factory;
using Domain;
using Services.Exceptions.Domain;
using Services.Facade;
using System;
using System.Collections.Generic;

namespace BLL
{
    public sealed class OxigenoBLL
    {
        #region Singleton
        private readonly static OxigenoBLL _instance = new OxigenoBLL();

        public static OxigenoBLL Current
        {
            get
            {
                return _instance;
            }
        }

        private OxigenoBLL()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        /// <summary>
        /// Crear un registro de Oxigeno
        /// </summary>
        /// <param name="obj">Recibe un objeto Oxigeno</param>
        public void Add(Oxigeno obj)
        {
            try
            {
                Factory.Current.GetRepositoryOxigeno().Add(obj);
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new BLLException(ex));
            }
        }

        /// <summary>
        /// Actualiza un registro de Oxigeno
        /// </summary>
        /// <param name="obj">Recibe un objeto Oxigeno</param>
        public void Update(Oxigeno obj)
        {
            try
            {
                Factory.Current.GetRepositoryOxigeno().Update(obj);
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
            }
        }

        /// <summary>
        /// Obtener todos los registros de Oxigeno para un Paciente específico
        /// </summary>
        /// <param name="paciente">Recibe un objeto Paciente</param>
        /// <returns>Retorna una lista de objetos Oxigeno</returns>
        public IEnumerable<Oxigeno> GetAll(Paciente paciente)
        {
            try
            {
                string filterExpression = "IdPaciente = " + paciente.IdPaciente;

                return Factory.Current.GetRepositoryOxigeno().GetAll(filterExpression);
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }

        /// <summary>
        /// Obtener un regsitro de Oxigeno
        /// </summary>
        /// <param name="Id">Recibe el Id del registro de Oxigeno</param>
        /// <returns>Retorna una objeto Oxigeno</returns>
        public Oxigeno GetOne(int id)
        {
            try
            {
                Oxigeno oxigeno = new Oxigeno();
                oxigeno = Factory.Current.GetRepositoryOxigeno().GetOne(id);

                // Paciente asociado al registro de oxígeno
                oxigeno.Paciente = PacienteBLL.Current.GetOne(oxigeno.IdPaciente);

                return oxigeno;
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }

        /// <summary>
        /// Crear un registro de Oxigeno en FIREBASE
        /// </summary>
        /// <param name="obj">Recibe un objeto Oxigeno</param>
        public void AddFirebase(Oxigeno obj)
        {
            try
            {
                Factory.Current.GetRepositoryTestFirebase().Add(obj);
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new BLLException(ex));
            }
        }
    }

}

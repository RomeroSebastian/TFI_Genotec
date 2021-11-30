using DAL.Factory;
using Domain;
using Services.Exceptions.Domain;
using Services.Facade;
using System;
using System.Collections.Generic;

namespace BLL
{   
    public sealed class CardioBLL
    {
        #region Singleton
        private readonly static CardioBLL _instance = new CardioBLL();

        public static CardioBLL Current
        {
            get
            {
                return _instance;
            }
        }

        private CardioBLL()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        /// <summary>
        /// Crear un registro de Cardio
        /// </summary>
        /// <param name="obj">Recibe un objeto Cardio</param>
        public void Add(Cardio obj)
        {
            try
            {
                Factory.Current.GetRepositoryCardio().Add(obj);
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new BLLException(ex));
            }
        }

        /// <summary>
        /// Actualiza un registro de Cardio
        /// </summary>
        /// <param name = "obj" > Recibe un objeto Cardio</param>
        public void Update(Cardio obj)
        {
            try
            {
                Factory.Current.GetRepositoryCardio().Update(obj);
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
            }
        }

        /// <summary>
        /// Obtener todos los registros de Cardio para un Paciente específico
        /// </summary>
        /// <param name="paciente">Recibe un objeto Paciente</param>
        /// <returns>Retorna una lista de objetos Cardio</returns>
        public IEnumerable<Cardio> GetAll(Paciente paciente)
        {
            try
            {
                string filterExpression = "IdPaciente = " + paciente.IdPaciente;

                return Factory.Current.GetRepositoryCardio().GetAll(filterExpression);
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }

        /// <summary>
        /// Obtener un regsitro de Cardio
        /// </summary>
        /// <param name="Id">Recibe el Id del registro de Cardio</param>
        /// <returns>Retorna una objeto Cardio</returns>
        public Cardio GetOne(int id)
        {
            try
            {
                Cardio cardio = new Cardio();
                cardio = Factory.Current.GetRepositoryCardio().GetOne(id);

                // Paciente asociado al registro de cardio
                cardio.Paciente = PacienteBLL.Current.GetOne(cardio.IdPaciente);

                return cardio;
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }

    }

}

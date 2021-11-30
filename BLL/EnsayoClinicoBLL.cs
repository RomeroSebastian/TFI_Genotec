using DAL.Factory;
using Domain;
using Services.Exceptions.Domain;
using Services.Facade;
using Services.Logger.BLL;
using Services.Logger.Domain;
using System;
using System.Collections.Generic;

namespace BLL
{
    public sealed class EnsayoClinicoBLL
    {
        #region Singleton
        private readonly static EnsayoClinicoBLL _instance = new EnsayoClinicoBLL();

        public static EnsayoClinicoBLL Current
        {
            get
            {
                return _instance;
            }
        }

        private EnsayoClinicoBLL()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        /// <summary>
        /// Crear un EnsayoClinico
        /// </summary>
        /// <param name="obj">Recibe un objeto EnsayoClinico</param>
        public void Add(EnsayoClinico obj)
        {
            try
            {
                Factory.Current.GetRepositoryEnsayoClinico().Add(obj);

                Log log = new Log();
                log.Date = DateTime.Now;
                log.Message = "Se creó el Ensayo Clínico Id " + obj.IdEnsayoClinico;
                log.UserName = "Sebastián Romero";
                log.Severity = Log.severityType.Info;
                log.LogType = Log.logType.Business;

                LoggerBLL.Current.Write(log);

            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new BLLException(ex));
            }
        }

        /// <summary>
        /// Actualiza un EnsayoClinico
        /// </summary>
        /// <param name="obj">Recibe un objeto EnsayoClinico</param>
        public void Update(EnsayoClinico obj)
        {
            try
            {
                Factory.Current.GetRepositoryEnsayoClinico().Update(obj);

                Log log = new Log();
                log.Date = DateTime.Now;
                log.Message = "Se actualizó el Ensayo Clínico Id " + obj.IdEnsayoClinico;
                log.UserName = "Sebastián Romero";
                log.Severity = Log.severityType.Info;
                log.LogType = Log.logType.Business;

                LoggerBLL.Current.Write(log);
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
            }
        }

        /// <summary>
        /// Obtener todos los EnsayoClinicos para un Tratamiento específico
        /// </summary>
        /// <param name="ensayoClinico">Recibe un objeto HistoriaClinica</param>
        /// <returns>Retorna una lista de objetos EnsayoClinico</returns>
        public IEnumerable<EnsayoClinico> GetAll(Tratamiento tratamiento)
        {
            try
            {
                string filterExpression = "IdTratamiento = " + tratamiento.IdTratamiento;

                return Factory.Current.GetRepositoryEnsayoClinico().GetAll(filterExpression);
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }

        /// <summary>
        /// Obtener todos los EnsayoClinicos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EnsayoClinico> GetAll()
        {
            try
            {
                return Factory.Current.GetRepositoryEnsayoClinico().GetAll();
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }

        /// <summary>
        /// Obtener un EnsayoClinico con los datos de los pacieentes
        /// </summary>
        /// <param name="Id">Recibe el Id del EnsayoClinico</param>
        /// <returns>Retorna una objeto EnsayoClinico</returns>
        public EnsayoClinico GetOne(int id)
        {
            try
            {
                EnsayoClinico ensayoClinico = new EnsayoClinico();
                ensayoClinico = Factory.Current.GetRepositoryEnsayoClinico().GetOne(id);

                // Lista de PacienteEnsayo
                string expression = "IdEnsayoClinico = " + id;
                ensayoClinico.PacienteEnsayo = (List<PacienteEnsayo>)Factory.Current.GetRepositoryPacienteEnsayo().GetAll(expression);

                // Lista de Pacientes
                foreach (var item in ensayoClinico.PacienteEnsayo)
                {
                    item.paciente = PacienteBLL.Current.GetOne(item.IdPaciente);
                }

                return ensayoClinico;

            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }

        /// <summary>
        /// Obtener un EnsayoClinico sin datos de pacientes
        /// </summary>
        /// <param name="Id">Recibe el Id del EnsayoClinico</param>
        /// <returns>Retorna una objeto EnsayoClinico</returns>
        public EnsayoClinico GetOneAlone(int id)
        {
            try
            {
                EnsayoClinico ensayoClinico = new EnsayoClinico();
                ensayoClinico = Factory.Current.GetRepositoryEnsayoClinico().GetOne(id);

                return ensayoClinico;

            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }
    }

}

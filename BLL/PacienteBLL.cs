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
    public sealed class PacienteBLL
    {
        #region Singleton
        private readonly static PacienteBLL _instance = new PacienteBLL();

        public static PacienteBLL Current
        {
            get
            {
                return _instance;
            }
        }

        private PacienteBLL()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        /// <summary>
        /// Crear un Paciente
        /// </summary>
        /// <param name="obj">Recibe un objeto Paciente</param>
        public void Add(Paciente obj)
        {
            try
            {
                Factory.Current.GetRepositoryPaciente().Add(obj);

                Log log = new Log();
                log.Date = DateTime.Now;
                log.Message = "Se creó el Paciente Id " + obj.IdPaciente;
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
        /// Actualiza un Paciente
        /// </summary>
        /// <param name="obj">Recibe un objeto Paciente</param>
        public void Update(Paciente obj)
        {
            try
            {
                Factory.Current.GetRepositoryPaciente().Update(obj);

                Log log = new Log();
                log.Date = DateTime.Now;
                log.Message = "Se actualizó el Paciente Id " + obj.IdPaciente;
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
        /// Obtener todos los Pacientes para un Ensayo Clínico específico
        /// </summary>
        /// <param name="Paciente">Recibe un objeto HistoriaClinica</param>
        /// <returns>Retorna una lista de objetos Paciente</returns>
        public IEnumerable<Paciente> GetAll(int idensayoClinico)
        {
            try
            {
                string filterExpression = "IdEnsayoClinico = " + idensayoClinico;

                return Factory.Current.GetRepositoryPaciente().GetAll(filterExpression);
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }

        /// <summary>
        /// Obtener todos los Pacientes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Paciente> GetAll()
        {
            try
            {
                return Factory.Current.GetRepositoryPaciente().GetAll();
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }

        /// <summary>
        /// Obtener un Paciente
        /// </summary>
        /// <param name="Id">Recibe el Id del Paciente</param>
        /// <returns>Retorna una objeto Paciente</returns>
        public Paciente GetOne(int id)
        {
            try
            {                
                Paciente paciente = new Paciente();
                paciente = Factory.Current.GetRepositoryPaciente().GetOne(id);

                // Lista de registros Cardio del paciente
                string expression = "IdPaciente = " + paciente.IdPaciente;
                paciente.Cardio = (List<Cardio>)Factory.Current.GetRepositoryCardio().GetAll(expression);

                // Lista de registros Temperatura del paciente
                paciente.Temperatura = (List<Temperatura>)Factory.Current.GetRepositoryTemperatura().GetAll(expression);

                // Lista de registros Oxigeno del paciente
                paciente.Oxigeno = (List<Oxigeno>)Factory.Current.GetRepositoryOxigeno().GetAll(expression);

                // Lista de Ensayos Clínicos
                // Lista de PacienteEnsayo
                string expression2 = "IdPaciente = " + id;
                paciente.PacienteEnsayo = (List<PacienteEnsayo>)Factory.Current.GetRepositoryPacienteEnsayo().GetAll(expression2);
                // Lista de Ensayos Clínicos (sin los datos de pacientes)
                foreach (var item in paciente.PacienteEnsayo)
                {
                    item.ensayoClinico = EnsayoClinicoBLL.Current.GetOneAlone(item.IdEnsayoClinico);
                }

                return paciente;
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }
    }

}

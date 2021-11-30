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
    public sealed class PacienteEnsayoBLL
    {
        #region Singleton
        private readonly static PacienteEnsayoBLL _instance = new PacienteEnsayoBLL();

        public static PacienteEnsayoBLL Current
        {
            get
            {
                return _instance;
            }
        }

        private PacienteEnsayoBLL()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        /// <summary>
        /// Crear una relación Paciente-Ensayo
        /// </summary>
        /// <param name="obj">Recibe un objeto PacienteEnsayo</param>
        public void Add(PacienteEnsayo obj)
        {
            try
            {
                Factory.Current.GetRepositoryPacienteEnsayo().Add(obj);

                Log log = new Log();
                log.Date = DateTime.Now;
                log.Message = "Se creó la relación Paciente Id " + obj.IdPaciente + "Ensayo Id " + obj.IdEnsayoClinico;
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

    }

}

using Services.Exceptions.Domain;
using Services.Facade;
using Services.Logger.DAL.Factory;
using Services.Logger.Domain;
using System;
using System.Collections.Generic;

namespace Services.Logger.BLL
{
    /// <summary>
    /// Esta clase se usa para que otros procesos puedan registrar un log en bitácora
    /// </summary>
    public sealed class LoggerBLL
    {
        #region Singleton
        private static readonly LoggerBLL _instance = new LoggerBLL();

        private LoggerBLL()
        {
            //Implement here the initialization code
        }

        public static LoggerBLL Current
        {
            get { return _instance; }
        }
        #endregion

        /// <summary>
        /// Escribe un registro de Log en la base de datos
        /// </summary>
        /// <param name="log"></param>
        public void Write(Log log)
        {
            try
            {
                Factory.Current.GetILogger().Add(log);
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new BLLException(ex));
            }
        }

        /// <summary>
        /// Escribe un registro de Log JSON Serializado para DAL
        /// </summary>
        /// <param name="log"></param>
        public void Write(Log_DAL log_DAL)
        {
            try
            {
                Factory.Current.GetDALFileLogger().Add(log_DAL);
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new BLLException(ex));
            }
        }

        /// <summary>
        /// Escribe un registro de Log JSON Serializado para inicio de sesion
        /// </summary>
        /// <param name="log"></param>
        public void Write(Log_Sesion log_Sesion)
        {
            try
            {
                Factory.Current.GetFileLogger().Add(log_Sesion);
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new BLLException(ex));
            }
        }

        /// <summary>
        /// Obtener todos los registros de bitácora de la base de datos
        /// </summary>
        /// <returns></returns>
        public List<Log> GetAll()
        {
            try
            {
                List<Log> logs = new List<Log>();

                logs = (List<Log>)Factory.Current.GetILogger().GetAll();

                return logs;
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }

        /// <summary>
        /// Obtener todos los registros de bitácora en archivo JSON Serializado para inicio de sesion
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Log_Sesion> GetAllSerializabledSesion()
        {
            try
            {
                return Factory.Current.GetFileLogger().GetAll();
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }

        /// <summary>
        /// Obtener todos los registros de bitácora en archivo JSON Serializado para DAL
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Log_DAL> GetAllSerializabledDAL()
        {
            try
            {
                return Factory.Current.GetDALFileLogger().GetAll();
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }
    }
}

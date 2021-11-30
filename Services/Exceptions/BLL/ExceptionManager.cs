using Microsoft.AspNetCore.Http;
using Services.Exceptions.Domain;
using Services.Logger.BLL;
using Services.Logger.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Services.Exceptions.BLL
{
    /// <summary>
    /// Esta clase es para manejar las excpeciones de UI - BLL - DAL
    /// Dependiendo del caso realiza diferentes acciones
    /// </summary>
    internal sealed class ExceptionManager
    {
        #region Singleton
        private readonly static ExceptionManager _instance = new ExceptionManager();

        public static ExceptionManager Current
        {
            get
            {
                return _instance;
            }
        }

        private ExceptionManager()
        {
            //Implent here the initialization of your singleton           
        }
        #endregion

        private string userID = "Anónimo";

        //private List<Claim> claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
        
        public void Handle(Exception ex)
        {
            if (ex is BLLException)
            {
                Handle(ex as BLLException);
            }
            else if (ex is DALException)
            {
                Handle(ex as DALException);
            }
            else if (ex is UIException)
            {
                Handle(ex as UIException);
            }
            else
            {
                throw ex;
            }
        }

        private void Handle(BLLException ex)
        {
            //userID = claims?.FirstOrDefault(x => x.Type.Equals("UserName", StringComparison.OrdinalIgnoreCase))?.Value;

            if (ex.InnerException is DALException)
            {
                //Si viene de DAL reemplazo el mensaje y lanzo//
                throw new Exception("Error accediendo a base de datos.", ex);
            }
            else if (ex.IsBusinessException)
            {
                //Es una excepción de reglas de negocio en BLL//
                //Registro el evento en la bitácora
                Log log = new Log();
                log.Date = DateTime.Now;
                log.Message = ex.Message;
                if (userID == null)
                {
                    log.UserName = "No_Login";
                }
                else
                {
                    log.UserName = userID;
                }                
                log.Severity = Log.severityType.Warning;
                log.LogType = Log.logType.Business;
                LoggerBLL.Current.Write(log);

                //Relanzo
                throw ex;
            }
            else
            {
                //Es una excepción de BLL no especificada//
                //Registro el envento en la bitácora
                Log log = new Log();
                log.Date = DateTime.Now;
                log.Message = ex.InnerException.Message;
                if (userID == null)
                {
                    log.UserName = "No_Login";
                }
                else
                {
                    log.UserName = userID;
                }
                log.Severity = Log.severityType.Error;
                log.LogType = Log.logType.System;
                LoggerBLL.Current.Write(log);

                //Lanzo segundo mensaje de excepción.
                throw new Exception("Error de negocio. Contacte al SME.", ex);
            }
        }

        private void Handle(DALException ex)
        {

            //userID = claims?.FirstOrDefault(x => x.Type.Equals("UserName", StringComparison.OrdinalIgnoreCase))?.Value;

            //Registro el envento en la bitácora
            Log_DAL log_DAL = new Log_DAL();
            log_DAL.Date = DateTime.Now;
            log_DAL.Message = ex.InnerException.Message;
            if (userID == null)
            {
                log_DAL.UserName = "No_Login";
            }
            else
            {
                log_DAL.UserName = userID;
            }
            log_DAL.Severity = Log.severityType.Error;
            log_DAL.LogType = Log.logType.System;
            
            LoggerBLL.Current.Write(log_DAL);

            //Relanzo
            throw ex;
        }

        private void Handle(UIException ex)
        {

            //userID = claims?.FirstOrDefault(x => x.Type.Equals("UserName", StringComparison.OrdinalIgnoreCase))?.Value;

            //Registro el envento en la bitácora
            Log log = new Log();
            log.Date = DateTime.Now;
            log.Message = ex.Message;
            if (userID == null)
            {
                log.UserName = "No_Login";
            }
            else
            {
                log.UserName = userID;
            }
            log.Severity = Log.severityType.Warning;
            log.LogType = Log.logType.Business;
            LoggerBLL.Current.Write(log);
        }

    }
}

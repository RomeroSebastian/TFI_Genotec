using Services.Exceptions.Domain;
using Services.Facade;
using Services.Logger.DAL.Contracts;
using Services.Logger.Domain;
using Services.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static Services.Logger.Domain.Log;

namespace Services.Logger.DAL.LogRepository
{
    /// <summary>
    /// Esta clase registra el log en una tabla de base de datos
    /// </summary>
    internal class SqlLogger : ILogger<Log>
    {

        //SQL Statements
        #region Statements
        private static string selectAll
        {
            get => "SELECT * FROM [dbo].[Log] order by Date desc";
        }

        private static string insertStatement
        {
            get => "INSERT INTO [dbo].[Log] VALUES (@date, @message, @user, @severity, @logtype);";
        }
        #endregion

        /// <summary>
        /// INSERT de un registro de Log
        /// </summary>
        /// <param name="log"></param>
        public void Add(Log log)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(new SqlParameter("date", log.Date));
                parametros.Add(new SqlParameter("message", log.Message));
                parametros.Add(new SqlParameter("user", log.UserName));
                parametros.Add(new SqlParameter("severity", log.Severity));
                parametros.Add(new SqlParameter("logtype", log.LogType));

                SqlHelper.ExecuteScalar(insertStatement, CommandType.Text, parametros.ToArray());

            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
            }
        }

        /// <summary>
        /// SELECT de todos los Logs en la tabla
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Log> GetAll()
        {
            try
            {
                List<Log> logs = new List<Log>();

                using (var dr = SqlHelper.ExecuteReader(selectAll, CommandType.Text))
                {

                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);

                        //Adapta los valores "values" que vienen en el array a un objeto Usuario
                        Log log = new Log();
                                               
                        log.Id = Convert.ToInt32(values[0]);
                        log.Date = Convert.ToDateTime(values[1]);
                        log.Message = values[2].ToString();
                        log.UserName = values[3].ToString();
                        log.Severity = (severityType)values[4];
                        log.LogType = (logType)values[5];

                        logs.Add(log);
                    }
                }               

                return logs;
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }
    }
}

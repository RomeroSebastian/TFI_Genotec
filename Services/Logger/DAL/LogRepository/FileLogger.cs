using Newtonsoft.Json;
using Services.Logger.DAL.Contracts;
using Services.Logger.Domain;
using System;
using System.Collections.Generic;
using System.IO;

namespace Services.Logger.DAL.LogRepository
{
    /// <summary>
    /// Esta clase genera un registro de log serializado en un archivo
    /// </summary>
    internal class FileLogger : ILogger<Log_Sesion>
    {
        private string path = ApplicationSettings.file_session;

        /// <summary>
        /// Agrega un registro de log en archivo JSON serializable
        /// </summary>
        /// <param name="obj"></param>
        public void Add(Log_Sesion objLog)
        {
            string outputJson = JsonConvert.SerializeObject(objLog);

            File.AppendAllText(path, outputJson + Environment.NewLine);
        }

        /// <summary>
        /// Obtiene todos los registros de log
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Log_Sesion> GetAll()
        {
            List<Log_Sesion> listLogSesions = new List<Log_Sesion>();

            using (StreamReader streamReader = new StreamReader(path))
            {
                while (streamReader.Peek() >= 0)
                {
                    string jsonString = streamReader.ReadLine();
                    var log_Sesion = JsonConvert.DeserializeObject<Log_Sesion>(jsonString);
                    listLogSesions.Add(log_Sesion);
                }
            }

            return listLogSesions;
        }
    }
}

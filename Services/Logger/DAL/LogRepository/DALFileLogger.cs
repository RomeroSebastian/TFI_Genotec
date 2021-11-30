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
    internal class DALFileLogger : ILogger<Log_DAL>
    {
        private string path = ApplicationSettings.file_dal;

        /// <summary>
        /// Agrega un registro de log en archivo JSON serializable
        /// </summary>
        /// <param name="obj"></param>
        public void Add(Log_DAL objLog)
        {
            string outputJson = JsonConvert.SerializeObject(objLog);

            File.AppendAllText(path, outputJson + Environment.NewLine);
        }

        /// <summary>
        /// Obtiene todos los registros de log del archivo JSON serializable
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Log_DAL> GetAll()
        {
            List<Log_DAL> listLogDAL = new List<Log_DAL>();

            using (StreamReader streamReader = new StreamReader(path))
            {
                while (streamReader.Peek() >= 0)
                {
                    string jsonString = streamReader.ReadLine();
                    var log_DAL = JsonConvert.DeserializeObject<Log_DAL>(jsonString);
                    listLogDAL.Add(log_DAL);
                }
            }

            return listLogDAL;
        }
    }
}

using Services.Logger.DAL.Contracts;
using Services.Logger.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Logger.DAL.Factory
{
    /// <summary>
	/// Clase tipo Factory para la bitácora
	/// </summary>
	public sealed class Factory
    {
		#region Singleton
		private static readonly Factory _instance = new Factory();

		private Factory()
		{
			//Implement here the initialization code
		}

		public static Factory Current
		{
			get { return _instance; }
		}
		#endregion

		/// <summary>
		/// LOG general en base de datos
		/// </summary>
		/// <returns></returns>
		public ILogger<Log> GetILogger()
		{

			string nombreNamespaceClaseAccesoRepo = ApplicationSettings.repoLogger + ".SqlLogger";

			object instancia = Activator.CreateInstance(Type.GetType(nombreNamespaceClaseAccesoRepo));

			return (ILogger<Log>)instancia;

		}

		/// <summary>
		/// LOG SERIALIZADO para Sesion
		/// </summary>
		/// <returns></returns>
		public ILogger<Log_Sesion> GetFileLogger()
		{

			string nombreNamespaceClaseAccesoRepo = ApplicationSettings.repoLogger + ".FileLogger";

			object instancia = Activator.CreateInstance(Type.GetType(nombreNamespaceClaseAccesoRepo));

			return (ILogger<Log_Sesion>)instancia;

		}

		/// <summary>
		/// LOG SERIALIZADO para DAL
		/// </summary>
		/// <returns></returns>
		public ILogger<Log_DAL> GetDALFileLogger()
		{

			string nombreNamespaceClaseAccesoRepo = ApplicationSettings.repoLogger + ".DALFileLogger";

			object instancia = Activator.CreateInstance(Type.GetType(nombreNamespaceClaseAccesoRepo));

			return (ILogger<Log_DAL>)instancia;

		}
	}
}

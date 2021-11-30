using System.Configuration;
using System.Reflection;
using System.IO;
using System;

namespace Services
{
    /// <summary>
	/// Esta clase obtiene los valores seteados en el app.config para que puedan ser usados por las otras clases.
	/// </summary>
	public static class ApplicationSettings
    {
		
		/// <summary>
		/// Ruta del directorio donde el usuario ejecuta la aplicación
		/// </summary>
		public static string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

		/// <summary>
		/// Conexión a base de datos de negocio
		/// </summary>
		public static string connBusiness = ConfigurationManager.ConnectionStrings["Connection_Business"].ToString();

		/// <summary>
		/// Conexión a base de datos de servicio
		/// </summary>
		public static string connServices = ConfigurationManager.ConnectionStrings["Connection_Services"].ToString();

		/// <summary>
		/// Repositorio datos de negocio
		/// </summary>
		public static string repoBusiness = ConfigurationManager.AppSettings["RepositoriesBusiness"].ToString();

		/// <summary>
		/// Repositorio datos de Firebase
		/// </summary>
		public static string repoFirebase = ConfigurationManager.AppSettings["RepositoriesFirebase"].ToString();

		/// <summary>
		/// Repositorio Logger o Bitacora
		/// </summary>
		public static string repoLogger = ConfigurationManager.AppSettings["RepositoriesLogger"].ToString();

		/// <summary>
		/// Ruta a archivos adjuntos
		/// </summary>
		public static string path_attachments = appPath + ConfigurationManager.AppSettings["Path_Attachments"].ToString();

		/// <summary>
		/// Archivo JSON Serializable para Log de Sesion
		/// </summary>
		public static string file_session = appPath + ConfigurationManager.AppSettings["File_Session"].ToString();

		/// <summary>
		/// Archivo JSON Serializable para Log de DAL
		/// </summary>
		public static string file_dal = appPath + ConfigurationManager.AppSettings["File_DAL"].ToString();

		/// <summary>
		/// Conexion a Firebase - Path
		/// </summary>
		public static string firebase_path = ConfigurationManager.AppSettings["Firebase_Path"].ToString();

		/// <summary>
		/// Conexion a Firebase - Secret
		/// </summary>
		public static string firebase_secret = ConfigurationManager.AppSettings["Firebase_Secret"].ToString();
	}
}

using Services.Exceptions.BLL;
using System;

namespace Services.Facade
{
    /// <summary>
    /// Clase tipo Facade para el manejo de excpeciones
    /// </summary>
    public static class FacadeServices
    {
        /// <summary>
        /// Manejo de excepciones
        /// </summary>
        /// <param name="ex"></param>
        public static void ManageException(Exception ex)
        {
            ExceptionManager.Current.Handle(ex);
        }
    }
}

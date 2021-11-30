using DAL.Factory;
using Domain;
using Services.Exceptions.Domain;
using Services.Facade;
using System;
using System.Collections.Generic;

namespace BLL
{

    public sealed class TratamientoBLL
    {
        #region Singleton
        private readonly static TratamientoBLL _instance = new TratamientoBLL();

        public static TratamientoBLL Current
        {
            get
            {
                return _instance;
            }
        }

        private TratamientoBLL()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

#nullable enable
        /// <summary>
        /// Crear un Tratamiento
        /// </summary>
        /// <param name="obj">Recibe un objeto Tratamiento</param>
        public void Add(Tratamiento obj)
        {
            try
            {
                Factory.Current.GetRepositoryTratamiento().Add(obj);

            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new BLLException(ex));
            }
        }

        /// <summary>
        /// Actualiza un Tratamiento
        /// </summary>
        /// <param name="obj">Recibe un objeto Tratamiento</param>
        public void Update(Tratamiento obj)
        {
            try
            {
                Factory.Current.GetRepositoryTratamiento().Update(obj);
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
            }
        }

        /// <summary>
        /// Obtener todos los Tratamientos para un...
        /// </summary>
        /// <param name="ensayoClinico">Recibe un objeto HistoriaClinica</param>
        /// <returns>Retorna una lista de objetos Tratamiento</returns>
        //public IEnumerable<Tratamiento> GetAll(EnsayoClinico ensayoClinico)
        //{
        //    try
        //    {
        //        string filterExpression = "IdEnsayoClinico = " + ensayoClinico.IdEnsayoClinico;

        //        return Factory.Current.GetRepositoryTratamiento().GetAll(filterExpression);
        //    }
        //    catch (Exception ex)
        //    {

        //        FacadeServices.ManageException(new BLLException(ex));
        //        return null;
        //    }
        //}

        public IEnumerable<Tratamiento> GetAll()
        {
            try
            {
                return Factory.Current.GetRepositoryTratamiento().GetAll();
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }

        /// <summary>
        /// Obtener un Tratamiento
        /// </summary>
        /// <param name="Id">Recibe el Id del Tratamiento</param>
        /// <returns>Retorna una objeto Tratamiento</returns>
        public Tratamiento GetOne(int id)
        {
            try
            {
                return Factory.Current.GetRepositoryTratamiento().GetOne(id);
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new BLLException(ex));
                return null;
            }
        }
    }

}

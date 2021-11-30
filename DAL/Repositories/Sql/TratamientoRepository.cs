using DAL.Contracts;
using DAL.Repositories.Sql.Adapters;
using DAL.Tools;
using Domain;
using Services.Exceptions.Domain;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Repositories.Sql
{
    /// <summary>
    /// Clase para realizar Select, Insert y Update en la tabla Tratamiento de la base de datos
    /// </summary>
    internal class TratamientoRepository : ITratamientoRepository<Tratamiento>
    {

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Tratamiento] VALUES (@Nombre, @Descripcion ,@Indicaciones, @Fecha)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Tratamiento] " +
                "SET Nombre = @Nombre, Descripcion = @Descripcion, Indicaciones = @Indicaciones, Fecha = @Fecha" +
                " WHERE IdTratamiento = @IdTratamiento";
        }

        private string SelectOneStatement
        {
            get => "SELECT * FROM [dbo].[Tratamiento] WHERE IdTratamiento = @IdTratamiento";
        }

        private string SelectAllStatement
        {
            get => "SELECT * FROM [dbo].[Tratamiento]";
        }
        #endregion


        /// <summary>
        /// SELECT de Tratamientos con Expression
        /// </summary>
        /// <param name="filterExpression">Puede recibir un string con la sentencia que se quiere agregar luego del where</param>
        /// <returns>Retorna una lista de objetos Tratamiento</returns>
        public IEnumerable<Tratamiento> GetAll(string filterExpression)
        {
            try
            {
                string sqlStatement = filterExpression ?? SelectAllStatement;

                sqlStatement = (sqlStatement == filterExpression) ? SelectAllStatement + " where " + filterExpression : sqlStatement;

                List<Tratamiento> tratamientos = new List<Tratamiento>();

                using (var dr = SqlHelper.ExecuteReader(sqlStatement, CommandType.Text))
                {
                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        tratamientos.Add(TratamientoAdapter.Current.Adapt(values));
                    }
                    return tratamientos;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        /// <summary>
        /// SELECT todos los Tratamientos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tratamiento> GetAll()
        {
            try
            {
                List<Tratamiento> tratamientos = new List<Tratamiento>();

                using (var dr = SqlHelper.ExecuteReader(SelectAllStatement, CommandType.Text))
                {
                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        tratamientos.Add(TratamientoAdapter.Current.Adapt(values));
                    }
                    return tratamientos;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        /// <summary>
        /// SELECT ONE. Selecciona un solo registro de la tabla Tratamiento.
        /// </summary>
        /// <param name="id">Recibe el Id de un Tratamiento</param>
        /// <returns>Retorna un objeto Tratamiento</returns>
        public Tratamiento GetOne(int id)
        {
            try
            {
                using (var dr = SqlHelper.ExecuteReader(SelectOneStatement, CommandType.Text,
                                      new SqlParameter[] { new SqlParameter("@IdTratamiento", id) }))
                {
                    Object[] values = new Object[dr.FieldCount];

                    Tratamiento tratamiento = default;

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        tratamiento = TratamientoAdapter.Current.Adapt(values);
                    }

                    return tratamiento;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        /// <summary>
        /// INSERT. Ejecuta un insert en la tabla Tratamiento
        /// </summary>
        /// <param name="obj">Recibe un objeto Tratamiento</param>
        public void Add(Tratamiento obj)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                
                parametros.Add(new SqlParameter("@Nombre", obj.Nombre));
                parametros.Add(new SqlParameter("@Descripcion", obj.Descripcion));
                parametros.Add(new SqlParameter("@Indicaciones", obj.Indicaciones));
                parametros.Add(new SqlParameter("@Fecha", obj.Fecha));

                SqlHelper.ExecuteScalar(InsertStatement, CommandType.Text, parametros.ToArray());
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new DALException(ex));
            }
        }

        /// <summary>
        /// UPDATE. Ejecuta un update sobre la tabla Tratamiento
        /// </summary>
        /// <param name="obj">Recibe un objeto Tratamiento</param>
        public void Update(Tratamiento obj)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(new SqlParameter("@IdTratamiento", obj.IdTratamiento));
                parametros.Add(new SqlParameter("@Nombre", obj.Nombre));
                parametros.Add(new SqlParameter("@Descripcion", obj.Descripcion));
                parametros.Add(new SqlParameter("@Indicaciones", obj.Indicaciones));
                parametros.Add(new SqlParameter("@Fecha", obj.Fecha));

                SqlHelper.ExecuteNonQuery(UpdateStatement, CommandType.Text, parametros.ToArray());
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
            }
        }
    }
}

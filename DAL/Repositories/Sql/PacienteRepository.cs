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
    /// Clase para realizar Select, Insert y Update en la tabla Paciente de la base de datos
    /// </summary>
    internal class PacienteRepository : IPacienteRepository<Paciente>
    {

        #region Statements        
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Paciente] " 
                + "(Nombre, Apellido, Documento, Domicilio, Telefono, Email, Genero, FechaNacimiento) " 
                + "VALUES (@Nombre, @Apellido, @Documento, @Domicilio, @Telefono, @Email, @Genero, @FechaNacimiento);";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Paciente] SET " 
                + "Nombre = @Nombre, Apellido = @Apellido, Documento = @Documento, Domicilio = @Domicilio, Telefono = @Telefono, Email = @Email, Genero = @Genero, FechaNacimiento = @FechaNacimiento "
                + "WHERE IdPaciente = @IdPaciente";
        }

        private string SelectOneStatement
        {
            get => "SELECT * FROM [dbo].[Paciente] WHERE IdPaciente = @IdPaciente";
        }

        private string SelectAllStatement
        {
            get => "SELECT * FROM [dbo].[Paciente]";
        }
        #endregion


        /// <summary>
        /// SELECT Pacientes con EXPRESSION
        /// </summary>
        /// <param name="filterExpression">Puede recibir un string con la sentencia que se quiere agregar luego del where</param>
        /// <returns>Retorna una lista de objetos Paciente</returns>
        public IEnumerable<Paciente> GetAll(string filterExpression)
        {
            try
            {
                string sqlStatement = filterExpression ?? SelectAllStatement;

                sqlStatement = (sqlStatement == filterExpression) ? SelectAllStatement + " where " + filterExpression : sqlStatement;

                List<Paciente> pacientes = new List<Paciente>();

                using (var dr = SqlHelper.ExecuteReader(sqlStatement, CommandType.Text))
                {
                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        pacientes.Add(PacienteAdapter.Current.Adapt(values));
                    }
                    return pacientes;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        /// <summary>
        /// SELECT Pacientes
        /// </summary>
        /// <returns>Retorna una lista de objetos Paciente</returns>
        public IEnumerable<Paciente> GetAll()
        {
            try
            {
                List<Paciente> pacientes = new List<Paciente>();

                using (var dr = SqlHelper.ExecuteReader(SelectAllStatement, CommandType.Text))
                {
                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        pacientes.Add(PacienteAdapter.Current.Adapt(values));
                    }
                    return pacientes;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        /// <summary>
        /// SELECT ONE. Selecciona un solo registro de la tabla Paciente.
        /// </summary>
        /// <param name="id">Recibe el Id de una Paciente</param>
        /// <returns>Retorna un objeto Paciente</returns>
        public Paciente? GetOne(int idPaciente)
        {
            try
            {
                using (var dr = SqlHelper.ExecuteReader(SelectOneStatement, CommandType.Text,
                                      new SqlParameter[] { new SqlParameter("@IdPaciente", idPaciente) }))
                {
                    Object[] values = new Object[dr.FieldCount];

                    Paciente? paciente = default;

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        paciente = PacienteAdapter.Current.Adapt(values);
                    }

                    return paciente;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }            
        }

        /// <summary>
        /// UPDATE. Ejecuta un update sobre la tabla Paciente
        /// </summary>
        /// <param name="obj">Recibe un objeto Paciente</param>
        public void Update(Paciente paciente)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(new SqlParameter("@IdPaciente", paciente.IdPaciente));
                parametros.Add(new SqlParameter("@Nombre", paciente.Nombre));
                parametros.Add(new SqlParameter("@Apellido", paciente.Apellido));
                parametros.Add(new SqlParameter("@Documento", paciente.Documento));
                parametros.Add(new SqlParameter("@Domicilio", paciente.Domicilio));
                parametros.Add(new SqlParameter("@Telefono", paciente.Telefono));
                parametros.Add(new SqlParameter("@Email", paciente.Email));
                parametros.Add(new SqlParameter("@Genero", paciente.Genero));
                parametros.Add(new SqlParameter("@FechaNacimiento", paciente.FechaNacimiento));

                SqlHelper.ExecuteNonQuery(UpdateStatement, CommandType.Text, parametros.ToArray());

            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
            }
        }

        /// <summary>
        /// INSERT. Ejecuta un insert en la tabla Paciente
        /// </summary>
        /// <param name="obj">Recibe un objeto Paciente</param>
        public void Add(Paciente paciente)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(new SqlParameter("@Nombre", paciente.Nombre));
                parametros.Add(new SqlParameter("@Apellido", paciente.Apellido));
                parametros.Add(new SqlParameter("@Documento", paciente.Documento));
                parametros.Add(new SqlParameter("@Domicilio", paciente.Domicilio));
                parametros.Add(new SqlParameter("@Telefono", paciente.Telefono));
                parametros.Add(new SqlParameter("@Email", paciente.Email));
                parametros.Add(new SqlParameter("@Genero", paciente.Genero));
                parametros.Add(new SqlParameter("@FechaNacimiento", paciente.FechaNacimiento));

                SqlHelper.ExecuteScalar(InsertStatement, CommandType.Text, parametros.ToArray());
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new DALException(ex));
            }
        }
    }
}

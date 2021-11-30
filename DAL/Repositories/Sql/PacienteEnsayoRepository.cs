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
    internal class PacienteEnsayoRepository : IPacienteEnsayoRepository<PacienteEnsayo>
    {

        #region Statements        
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[PacienteEnsayoClinico] (IdPaciente, IdEnsayoClinico) VALUES (@IdPaciente, @IdEnsayoClinico);";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[PacienteEnsayoClinico] SET IdPaciente = @IdPaciente, IdEnsayoClinico = @IdEnsayoClinico WHERE IdPaciente = @IdPaciente AND IdEnsayoClinico = @IdEnsayoClinico";
        }

        private string SelectOneStatement
        {
            get => "SELECT * FROM [dbo].[PacienteEnsayoClinico] WHERE IdPaciente = @IdPaciente AND IdEnsayoClinico = @IdEnsayoClinico";
        }

        private string SelectAllStatement
        {
            get => "SELECT * FROM [dbo].[PacienteEnsayoClinico]";
        }
        #endregion

        /// <summary>
        /// INSERT. Ejecuta un insert en la tabla PacienteEnsayo
        /// </summary>
        /// <param name="PacienteEnsayo"></param>
        public void Add(PacienteEnsayo pacienteEnsayo)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(new SqlParameter("@IdPaciente", pacienteEnsayo.IdPaciente));
                parametros.Add(new SqlParameter("@IdEnsayoClinico", pacienteEnsayo.IdEnsayoClinico));

                SqlHelper.ExecuteScalar(InsertStatement, CommandType.Text, parametros.ToArray());
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new DALException(ex));
            }
        }

        /// <summary>
        /// SELECT PacienteEnsayo con expresión
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public IEnumerable<PacienteEnsayo> GetAll(string filterExpression)
        {
            try
            {
                string sqlStatement = filterExpression ?? SelectAllStatement;

                sqlStatement = (sqlStatement == filterExpression) ? SelectAllStatement + " where " + filterExpression : sqlStatement;

                List<PacienteEnsayo> pacientesEnsayos = new List<PacienteEnsayo>();

                using (var dr = SqlHelper.ExecuteReader(sqlStatement, CommandType.Text))
                {
                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        pacientesEnsayos.Add(PacienteEnsayoAdapter.Current.Adapt(values));
                    }
                    return pacientesEnsayos;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        /// <summary>
        /// SELECT PacienteEnsayo
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PacienteEnsayo> GetAll()
        {
            try
            {
                List<PacienteEnsayo> pacientesEnsayos = new List<PacienteEnsayo>();

                using (var dr = SqlHelper.ExecuteReader(SelectAllStatement, CommandType.Text))
                {
                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        pacientesEnsayos.Add(PacienteEnsayoAdapter.Current.Adapt(values));
                    }
                    return pacientesEnsayos;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        /// <summary>
        /// SELECT ONE. Selecciona un solo registro de la tabla PacienteEnsayo
        /// </summary>
        /// <param name="idPacienteEnsayo"></param>
        /// <returns></returns>
        public PacienteEnsayo GetOne(int idPaciente, int idEnsayoClinico)
        {
            try
            {
                using (var dr = SqlHelper.ExecuteReader(SelectOneStatement, CommandType.Text,
                                      new SqlParameter[] { new SqlParameter("@IdPaciente", idPaciente),
                                      new SqlParameter("@IdEnsayoClinico", idEnsayoClinico) }))
                {
                    Object[] values = new Object[dr.FieldCount];

                    PacienteEnsayo? pacienteEnsayo = default;

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        pacienteEnsayo = PacienteEnsayoAdapter.Current.Adapt(values);
                    }

                    return pacienteEnsayo;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        /// <summary>
        /// UPDATE. Actualiza un registro de PacienteEnsayo
        /// </summary>
        /// <param name="pacienteEnsayo"></param>
        public void Update(PacienteEnsayo pacienteEnsayo)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(new SqlParameter("@IdPaciente", pacienteEnsayo.IdPaciente));
                parametros.Add(new SqlParameter("@IdEnsayoClinico", pacienteEnsayo.IdEnsayoClinico));

                SqlHelper.ExecuteNonQuery(UpdateStatement, CommandType.Text, parametros.ToArray());

            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
            }
        }
    }
}

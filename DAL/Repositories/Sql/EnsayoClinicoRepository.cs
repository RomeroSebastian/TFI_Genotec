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
    internal class EnsayoClinicoRepository : IEnsayoClinicoRepository<EnsayoClinico>
    {

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[EnsayoClinico] VALUES " + 
                "(@CodigoSISA, @Titulo, @CondicionSalud, @Categoria, @Tipo, @Fase, @FechaRegistro, @FechaInicio ,@FechaFin, @Estado)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[EnsayoClinico] SET " +
                "CodigoSISA = @CodigoSISA, Titulo = @Titulo, CondicionSalud = @CondicionSalud, Categoria = @Categoria, Tipo = @Tipo, Fase = @Fase, " + 
                "FechaRegistro = @FechaRegistro, FechaInicio = @FechaInicio , FechaFin = @FechaFin, Estado = @Estado " + 
                "WHERE IdEnsayoClinico = @IdEnsayoClinico";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[EnsayoClinico] WHERE IdEnsayoClinico = @IdEnsayoClinico";
        }

        private string SelectOneStatement
        {
            get => "SELECT * FROM [dbo].[EnsayoClinico] WHERE IdEnsayoClinico = @IdEnsayoClinico";
        }

        private string SelectAllStatement
        {
            get => "SELECT * FROM [dbo].[EnsayoClinico]";
        }
        #endregion

        /// <summary>
        /// INSERT de un registro de Ensayo Clinico
        /// </summary>
        /// <param name="ensayoClinico"></param>
        public void Add(EnsayoClinico ensayoClinico)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(new SqlParameter("@CodigoSISA", ensayoClinico.CodigoSISA));
                parametros.Add(new SqlParameter("@Titulo", ensayoClinico.Titulo));
                parametros.Add(new SqlParameter("@CondicionSalud", ensayoClinico.CondicionSalud));
                parametros.Add(new SqlParameter("@Categoria", ensayoClinico.Categoria));
                parametros.Add(new SqlParameter("@Tipo", ensayoClinico.Tipo));
                parametros.Add(new SqlParameter("@Fase", ensayoClinico.Fase));
                parametros.Add(new SqlParameter("@FechaRegistro", ensayoClinico.FechaRegistro));
                parametros.Add(new SqlParameter("@FechaInicio", ensayoClinico.FechaInicio));
                if (ensayoClinico.FechaFin == null || ensayoClinico.FechaFin < Convert.ToDateTime("01/01/1900"))
                {
                    parametros.Add(new SqlParameter("@FechaFin", DBNull.Value));
                }
                else
                {
                    parametros.Add(new SqlParameter("@FechaFin", ensayoClinico.FechaFin));
                }
                parametros.Add(new SqlParameter("@Estado", ensayoClinico.Estado));
                //parametros.Add(new SqlParameter("@IdTratamiento", ensayoClinico.tratamiento.IdTratamiento));

                SqlHelper.ExecuteScalar(InsertStatement, CommandType.Text, parametros.ToArray());
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
            }
        }

        /// <summary>
        /// SELECT Ensayo Clinico con EXPRESSION
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public IEnumerable<EnsayoClinico> GetAll(string filterExpression)
        {
            try
            {
                string sqlStatement = filterExpression ?? SelectAllStatement;

                sqlStatement = (sqlStatement == filterExpression) ? SelectAllStatement + " where " + filterExpression : sqlStatement;

                List<EnsayoClinico> ensayoClinicos = new List<EnsayoClinico>();

                using (var dr = SqlHelper.ExecuteReader(sqlStatement, CommandType.Text))
                {
                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        ensayoClinicos.Add(EnsayoClinicoAdapter.Current.Adapt(values));
                    }
                    return ensayoClinicos;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        /// <summary>
        /// SELECT todos los Ensayos Clinicos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EnsayoClinico> GetAll()
        {
            try
            {
                List<EnsayoClinico> ensayoClinicos = new List<EnsayoClinico>();

                using (var dr = SqlHelper.ExecuteReader(SelectAllStatement, CommandType.Text))
                {
                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        ensayoClinicos.Add(EnsayoClinicoAdapter.Current.Adapt(values));
                    }
                    return ensayoClinicos;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        /// <summary>
        /// SELECT un registro de Ensayo Clinico
        /// </summary>
        /// <param name="idEnsayoClinico"></param>
        /// <returns></returns>
        public EnsayoClinico GetOne(int idEnsayoClinico)
        {
            try
            {
                using (var dr = SqlHelper.ExecuteReader(SelectOneStatement, CommandType.Text,
                                      new SqlParameter[] { new SqlParameter("@IdEnsayoClinico", idEnsayoClinico) }))
                {
                    Object[] values = new Object[dr.FieldCount];

                    EnsayoClinico ensayoClinico = default;

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        ensayoClinico = EnsayoClinicoAdapter.Current.Adapt(values);
                    }

                    return ensayoClinico;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        /// <summary>
        /// UPDATE Ensayo Clinico
        /// </summary>
        /// <param name="ensayoClinico"></param>
        public void Update(EnsayoClinico ensayoClinico)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(new SqlParameter("@IdEnsayoClinico", ensayoClinico.IdEnsayoClinico));
                parametros.Add(new SqlParameter("@CodigoSISA", ensayoClinico.CodigoSISA));
                parametros.Add(new SqlParameter("@Titulo", ensayoClinico.Titulo));
                parametros.Add(new SqlParameter("@CondicionSalud", ensayoClinico.CondicionSalud));
                parametros.Add(new SqlParameter("@Categoria", ensayoClinico.Categoria));
                parametros.Add(new SqlParameter("@Tipo", ensayoClinico.Tipo));
                parametros.Add(new SqlParameter("@Fase", ensayoClinico.Fase));
                parametros.Add(new SqlParameter("@FechaRegistro", ensayoClinico.FechaRegistro));
                parametros.Add(new SqlParameter("@FechaInicio", ensayoClinico.FechaInicio));
                if (ensayoClinico.FechaFin == null || ensayoClinico.FechaFin < Convert.ToDateTime("01/01/1900"))
                {
                    parametros.Add(new SqlParameter("@FechaFin", DBNull.Value));
                }
                else
                {
                    parametros.Add(new SqlParameter("@FechaFin", ensayoClinico.FechaFin));
                }
                parametros.Add(new SqlParameter("@Estado", ensayoClinico.Estado));
                //parametros.Add(new SqlParameter("@IdTratamiento", ensayoClinico.tratamiento.IdTratamiento));

                SqlHelper.ExecuteNonQuery(UpdateStatement, CommandType.Text, parametros.ToArray());
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
            }
        }
    }
}

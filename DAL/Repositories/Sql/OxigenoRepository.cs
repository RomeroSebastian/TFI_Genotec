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
using System.Threading.Tasks;

namespace DAL.Repositories.Sql
{
    /// <summary>
    /// Clase para realizar Select, Insert y Update en la tabla Oxigeno de la base de datos
    /// </summary>
    internal class OxigenoRepository : IOxigenoRepository<Oxigeno>
    {

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Oxigeno] VALUES (@Valor, @Unidad, @Fecha, @IdPaciente)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Oxigeno] SET Valor = @Valor, Unidad = @Unidad, Fecha = @Fecha, IdPaciente = @IdPaciente WHERE  IdOxigeno = @IdOxigeno";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Oxigeno] WHERE IdOxigeno = @IdOxigeno";
        }

        private string SelectOneStatement
        {
            get => "SELECT * FROM [dbo].[Oxigeno] WHERE IdOxigeno = @IdOxigeno";
        }

        private string SelectAllStatement
        {
            get => "SELECT * FROM [dbo].[Oxigeno]";
        }
        #endregion

        public void Add(Oxigeno oxigeno)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(new SqlParameter("@Valor", oxigeno.Valor));
                parametros.Add(new SqlParameter("@Unidad", oxigeno.Unidad));
                parametros.Add(new SqlParameter("@Fecha", oxigeno.Fecha));
                parametros.Add(new SqlParameter("@IdPaciente", oxigeno.IdPaciente));

                SqlHelper.ExecuteScalar(InsertStatement, CommandType.Text, parametros.ToArray());
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new DALException(ex));
            }
        }

        public IEnumerable<Oxigeno> GetAll(string filterExpression)
        {
            try
            {
                string sqlStatement = filterExpression ?? SelectAllStatement;

                sqlStatement = (sqlStatement == filterExpression) ? SelectAllStatement + " where " + filterExpression : sqlStatement;

                List<Oxigeno> listOxigeno = new List<Oxigeno>();

                using (var dr = SqlHelper.ExecuteReader(sqlStatement, CommandType.Text))
                {
                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        listOxigeno.Add(OxigenoAdapter.Current.Adapt(values));
                    }
                    return listOxigeno;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        public IEnumerable<Oxigeno> GetAll()
        {
            try
            {
                List<Oxigeno> listOxigeno = new List<Oxigeno>();

                using (var dr = SqlHelper.ExecuteReader(SelectAllStatement, CommandType.Text))
                {
                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        listOxigeno.Add(OxigenoAdapter.Current.Adapt(values));
                    }
                    return listOxigeno;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        public Oxigeno GetOne(int idOxigeno)
        {
            try
            {
                using (var dr = SqlHelper.ExecuteReader(SelectOneStatement, CommandType.Text,
                                      new SqlParameter[] { new SqlParameter("@IdOxigeno", idOxigeno) }))
                {
                    Object[] values = new Object[dr.FieldCount];

                    Oxigeno? oxigeno = default;

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        oxigeno = OxigenoAdapter.Current.Adapt(values);
                    }

                    return oxigeno;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        public void Update(Oxigeno oxigeno)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(new SqlParameter("@IdOxigeno", oxigeno.IdOxigeno));
                parametros.Add(new SqlParameter("@Valor", oxigeno.Valor));
                parametros.Add(new SqlParameter("@Unidad", oxigeno.Unidad));
                parametros.Add(new SqlParameter("@Fecha", oxigeno.Fecha));
                parametros.Add(new SqlParameter("@IdPaciente", oxigeno.IdPaciente));

                SqlHelper.ExecuteNonQuery(UpdateStatement, CommandType.Text, parametros.ToArray());
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
            }
        }

        public void Delete(Oxigeno oxigeno)
        {
            throw new NotImplementedException();
        }
    }
}

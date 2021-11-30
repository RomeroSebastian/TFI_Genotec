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
    internal class TemperaturaRepository : ITemperaturaRepository<Temperatura>
    {
        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Temperatura] VALUES (@Valor, @Unidad, @Fecha, @IdPaciente)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Temperatura] SET Valor = @Valor, Unidad = @Unidad, Fecha = @Fecha, IdPaciente = @IdPaciente WHERE IdTemperatura = @IdTemperatura";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Temperatura] WHERE IdTemperatura = @IdTemperatura";
        }

        private string SelectOneStatement
        {
            get => "SELECT * FROM [dbo].[Temperatura] WHERE IdTemperatura = @IdTemperatura";
        }

        private string SelectAllStatement
        {
            get => "SELECT * FROM [dbo].[Temperatura]";
        }
        #endregion

        public void Add(Temperatura temperatura)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(new SqlParameter("@Valor", temperatura.Valor));
                parametros.Add(new SqlParameter("@Unidad", temperatura.Unidad));
                parametros.Add(new SqlParameter("@Fecha", temperatura.Fecha));
                parametros.Add(new SqlParameter("@IdPaciente", temperatura.IdPaciente));

                SqlHelper.ExecuteScalar(InsertStatement, CommandType.Text, parametros.ToArray());
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new DALException(ex));
            }
        }

        public IEnumerable<Temperatura> GetAll(string filterExpression)
        {
            try
            {
                string sqlStatement = filterExpression ?? SelectAllStatement;

                sqlStatement = (sqlStatement == filterExpression) ? SelectAllStatement + " where " + filterExpression : sqlStatement;

                List<Temperatura> listTemperatura = new List<Temperatura>();

                using (var dr = SqlHelper.ExecuteReader(sqlStatement, CommandType.Text))
                {
                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        listTemperatura.Add(TemperaturaAdapter.Current.Adapt(values));
                    }
                    return listTemperatura;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        public IEnumerable<Temperatura> GetAll()
        {
            try
            {
                List<Temperatura> listTemperatura = new List<Temperatura>();

                using (var dr = SqlHelper.ExecuteReader(SelectAllStatement, CommandType.Text))
                {
                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        listTemperatura.Add(TemperaturaAdapter.Current.Adapt(values));
                    }
                    return listTemperatura;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        public Temperatura GetOne(int idTemperatura)
        {
            try
            {
                using (var dr = SqlHelper.ExecuteReader(SelectOneStatement, CommandType.Text,
                                      new SqlParameter[] { new SqlParameter("@IdTemperatura", idTemperatura) }))
                {
                    Object[] values = new Object[dr.FieldCount];

                    Temperatura? temperatura = default;

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        temperatura = TemperaturaAdapter.Current.Adapt(values);
                    }

                    return temperatura;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        /// <summary>
        /// UPDATE Temperatura
        /// </summary>
        /// <param name="temperatura"></param>
        public void Update(Temperatura temperatura)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(new SqlParameter("@IdTemperatura", temperatura.IdTemperatura));
                parametros.Add(new SqlParameter("@Valor", temperatura.Valor));
                parametros.Add(new SqlParameter("@Unidad", temperatura.Unidad));
                parametros.Add(new SqlParameter("@Fecha", temperatura.Fecha));
                parametros.Add(new SqlParameter("@IdPaciente", temperatura.IdPaciente));

                SqlHelper.ExecuteNonQuery(UpdateStatement, CommandType.Text, parametros.ToArray());
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
            }
        }
    }
}

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
    /// Clase para realizar Select, Insert y Update en la tabla Cardio de la base de datos
    /// </summary>
    internal class CardioRepository : ICardioRepository<Cardio>
    {
        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Cardio] VALUES (@Valor, @Unidad, @Fecha, @IdPaciente)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Cardio] SET Valor = @Valor, Unidad = @Unidad, Fecha = @Fecha, IdPaciente = @IdPaciente WHERE  IdCardio = @IdCardio";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Cardio] WHERE IdCardio = @IdCardio";
        }

        private string SelectOneStatement
        {
            get => "SELECT * FROM [dbo].[Cardio] WHERE IdCardio = @IdCardio";
        }

        private string SelectAllStatement
        {
            get => "SELECT * FROM [dbo].[Cardio]";
        }
        #endregion

        public void Add(Cardio cardio)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(new SqlParameter("@Valor", cardio.Valor));
                parametros.Add(new SqlParameter("@Unidad", cardio.Unidad));
                parametros.Add(new SqlParameter("@Fecha", cardio.Fecha));
                parametros.Add(new SqlParameter("@IdPaciente", cardio.IdPaciente));

                SqlHelper.ExecuteScalar(InsertStatement, CommandType.Text, parametros.ToArray());
            }
            catch (Exception ex)
            {

                FacadeServices.ManageException(new DALException(ex));
            }
        }

        public IEnumerable<Cardio> GetAll(string filterExpression)
        {
            try
            {
                string sqlStatement = filterExpression ?? SelectAllStatement;

                sqlStatement = (sqlStatement == filterExpression) ? SelectAllStatement + " where " + filterExpression : sqlStatement;

                List<Cardio> listCardio = new List<Cardio>();

                using (var dr = SqlHelper.ExecuteReader(sqlStatement, CommandType.Text))
                {
                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        listCardio.Add(CardioAdapter.Current.Adapt(values));
                    }
                    return listCardio;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        public IEnumerable<Cardio> GetAll()
        {
            try
            {
                List<Cardio> listCardio = new List<Cardio>();

                using (var dr = SqlHelper.ExecuteReader(SelectAllStatement, CommandType.Text))
                {
                    Object[] values = new Object[dr.FieldCount];

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        listCardio.Add(CardioAdapter.Current.Adapt(values));
                    }
                    return listCardio;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        public Cardio GetOne(int idCardio)
        {
            try
            {
                using (var dr = SqlHelper.ExecuteReader(SelectOneStatement, CommandType.Text,
                                      new SqlParameter[] { new SqlParameter("@IdCardio", idCardio) }))
                {
                    Object[] values = new Object[dr.FieldCount];

                    Cardio? cardio = default;

                    while (dr.Read())
                    {
                        dr.GetValues(values);
                        cardio = CardioAdapter.Current.Adapt(values);
                    }

                    return cardio;
                }
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
                return null;
            }
        }

        public void Update(Cardio cardio)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(new SqlParameter("@IdCardio", cardio.IdCardio));
                parametros.Add(new SqlParameter("@Valor", cardio.Valor));
                parametros.Add(new SqlParameter("@Unidad", cardio.Unidad));
                parametros.Add(new SqlParameter("@Fecha", cardio.Fecha));
                parametros.Add(new SqlParameter("@IdPaciente", cardio.IdPaciente));

                SqlHelper.ExecuteNonQuery(UpdateStatement, CommandType.Text, parametros.ToArray());
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
            }
        }
    }
}

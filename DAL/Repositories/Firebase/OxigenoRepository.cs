using Domain;
using System;
using System.Threading.Tasks;
using DAL.Contracts;
using Services;
using Services.Exceptions.Domain;
using Services.Facade;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;

namespace DAL.Repositories.Firebase
{
    internal class OxigenoRepository : IOxigenoRepository<Oxigeno>
    {
        public void Add(Oxigeno oxigeno)
        {
            try
            {
                Insertar(oxigeno);
            }
            catch (Exception ex)
            {
                FacadeServices.ManageException(new DALException(ex));
            }
        }

        private async Task Insertar(Oxigeno oxigeno)
        {
            try
            {
                var firebase = new FirebaseClient(ApplicationSettings.firebase_path);

                var valor = await firebase
                    .Child("Oxigeno/")
                    .PostAsync(oxigeno);
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

        public IEnumerable<Oxigeno> GetAll(string filterExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Oxigeno> GetAll()
        {
            throw new NotImplementedException();
        }

        public Oxigeno GetOne(int idOxigeno)
        {
            throw new NotImplementedException();
        }

        public void Update(Oxigeno oxigeno)
        {
            throw new NotImplementedException();
        }
    }
}

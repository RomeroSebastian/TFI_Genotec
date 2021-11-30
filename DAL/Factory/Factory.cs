using DAL.Contracts;
using Domain;
using Services;
using System;

namespace DAL.Factory
{
	public sealed class Factory
    {

		#region Singleton
		private static readonly Factory _instance = new Factory();

		private Factory()
		{
			//Implement here the initialization code
		}

		public static Factory Current
		{
			get { return _instance; }
		}
		#endregion

		/// <summary>
		/// CARDIO Repository
		/// </summary>
		/// <returns>Retorna una instancia de CardioRepository</returns>
		public ICardioRepository<Cardio> GetRepositoryCardio()
		{

			string nombreNamespaceClaseAccesoRepo = ApplicationSettings.repoBusiness + ".CardioRepository";

			object instancia = Activator.CreateInstance(Type.GetType(nombreNamespaceClaseAccesoRepo));

			return (ICardioRepository<Cardio>)instancia;

		}

		/// <summary>
		/// ENSAYO CLINICO Repository
		/// </summary>
		/// <returns>Retorna una instancia de EnsayoClinicoRepository</returns>
		public IEnsayoClinicoRepository<EnsayoClinico> GetRepositoryEnsayoClinico()
		{

			string nombreNamespaceClaseAccesoRepo = ApplicationSettings.repoBusiness + ".EnsayoClinicoRepository";

			object instancia = Activator.CreateInstance(Type.GetType(nombreNamespaceClaseAccesoRepo));

			return (IEnsayoClinicoRepository<EnsayoClinico>)instancia;

		}

		/// <summary>
		/// MEDICACION Repository
		/// </summary>
		/// <returns>Retorna una instancia de MedicacionRepository</returns>
		public IMedicacionRepository<Medicacion> GetRepositoryMedicacion()
		{

			string nombreNamespaceClaseAccesoRepo = ApplicationSettings.repoBusiness + ".MedicacionRepository";

			object instancia = Activator.CreateInstance(Type.GetType(nombreNamespaceClaseAccesoRepo));

			return (IMedicacionRepository<Medicacion>)instancia;

		}

		/// <summary>
		/// OXIGENO Repository
		/// </summary>
		/// <returns>Retorna una instancia de OxigenoRepository</returns>
		public IOxigenoRepository<Oxigeno> GetRepositoryOxigeno()
		{

			string nombreNamespaceClaseAccesoRepo = ApplicationSettings.repoBusiness + ".OxigenoRepository";

			object instancia = Activator.CreateInstance(Type.GetType(nombreNamespaceClaseAccesoRepo));

			return (IOxigenoRepository<Oxigeno>)instancia;

		}

		/// <summary>
		/// PACIENTE Repository
		/// </summary>
		/// <returns>Retorna una instancia de PacienteRepository</returns>
		public IPacienteRepository<Paciente> GetRepositoryPaciente()
		{

			string nombreNamespaceClaseAccesoRepo = ApplicationSettings.repoBusiness + ".PacienteRepository";

			object instancia = Activator.CreateInstance(Type.GetType(nombreNamespaceClaseAccesoRepo));

			return (IPacienteRepository<Paciente>)instancia;

		}

		/// <summary>
		/// PACIENTE ENSAYO Repository
		/// </summary>
		/// <returns>Retorna una instancia de PacienteEnsayoRepository</returns>
		public IPacienteEnsayoRepository<PacienteEnsayo> GetRepositoryPacienteEnsayo()
		{

			string nombreNamespaceClaseAccesoRepo = ApplicationSettings.repoBusiness + ".PacienteEnsayoRepository";

			object instancia = Activator.CreateInstance(Type.GetType(nombreNamespaceClaseAccesoRepo));

			return (IPacienteEnsayoRepository<PacienteEnsayo>)instancia;

		}

		/// <summary>
		/// TEMPERATURA Repository
		/// </summary>
		/// <returns>Retorna una instancia de TemperaturaRepository</returns>
		public ITemperaturaRepository<Temperatura> GetRepositoryTemperatura()
		{

			string nombreNamespaceClaseAccesoRepo = ApplicationSettings.repoBusiness + ".TemperaturaRepository";

			object instancia = Activator.CreateInstance(Type.GetType(nombreNamespaceClaseAccesoRepo));

			return (ITemperaturaRepository<Temperatura>)instancia;

		}

		/// <summary>
		/// TRATAMIENTO Repository
		/// </summary>
		/// <returns>Retorna una instancia de TratamientoRepository</returns>
		public ITratamientoRepository<Tratamiento> GetRepositoryTratamiento()
		{

			string nombreNamespaceClaseAccesoRepo = ApplicationSettings.repoBusiness + ".TratamientoRepository";

			object instancia = Activator.CreateInstance(Type.GetType(nombreNamespaceClaseAccesoRepo));

			return (ITratamientoRepository<Tratamiento>)instancia;

		}

		/// <summary>
		/// FIREBASE Repository
		/// </summary>
		/// <returns>Retorna una instancia de TratamientoRepository</returns>
		public IOxigenoRepository<Oxigeno> GetRepositoryTestFirebase()
		{

			string nombreNamespaceClaseAccesoRepo = ApplicationSettings.repoFirebase + ".OxigenoRepository";

			object instancia = Activator.CreateInstance(Type.GetType(nombreNamespaceClaseAccesoRepo));

			return (IOxigenoRepository<Oxigeno>)instancia;

		}
	}
}

using DAL.Contracts;
using Domain;
using System;

namespace DAL.Repositories.Sql.Adapters
{
    public sealed class EnsayoClinicoAdapter : IEntityAdapter<EnsayoClinico>
    {
        #region Singleton
        private readonly static EnsayoClinicoAdapter _instance = new EnsayoClinicoAdapter();

        public static EnsayoClinicoAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private EnsayoClinicoAdapter()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public EnsayoClinico Adapt(object[] values)
        {
            EnsayoClinico ensayoClinico = new EnsayoClinico();

            ensayoClinico.IdEnsayoClinico = Convert.ToInt16(values[0]);
            ensayoClinico.CodigoSISA = values[1].ToString();
            ensayoClinico.Titulo = values[2].ToString();
            ensayoClinico.CondicionSalud = values[3].ToString();
            ensayoClinico.Categoria = values[4].ToString();
            ensayoClinico.Tipo = values[5].ToString();
            ensayoClinico.Fase = values[6].ToString();
            ensayoClinico.FechaRegistro = Convert.ToDateTime(values[7]);
            ensayoClinico.FechaInicio = Convert.ToDateTime(values[8]);
            if (values[9].ToString() != "")
                ensayoClinico.FechaFin = Convert.ToDateTime(values[9]);
            ensayoClinico.Estado = values[10].ToString();

            return ensayoClinico;
        }
    }
}

namespace DAL.Contracts
{
    /// <summary>
    /// Interfaz genérica para implementar clases Adapter
    /// </summary>
    /// <typeparam name="T">Recibe un parámetro genérico T</typeparam>
    internal interface IEntityAdapter<T> where T : class, new()
    {
        T Adapt(object[] values);
    }
}

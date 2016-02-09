namespace SimplePersistence.UoW
{
#if !(NET20 || NET35)

    /// <summary>
    /// Repository interface that will be used to query and manipulate
    /// persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The entity id type</typeparam>
    public interface IRepository<TEntity, in TId> : IAsyncRepository<TEntity, TId>, ISyncRepository<TEntity, TId>
        where TEntity : class
    {

    }

    /// <summary>
    /// Repository interface that will be used to query and manipulate
    /// persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    public interface IRepository<TEntity, in TId01, in TId02> : IAsyncRepository<TEntity, TId01, TId02>, ISyncRepository<TEntity, TId01, TId02>
        where TEntity : class
    {

    }

    /// <summary>
    /// Repository interface that will be used to query and manipulate
    /// persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    /// <typeparam name="TId03">The entity id third type</typeparam>
    public interface IRepository<TEntity, in TId01, in TId02, in TId03> : IAsyncRepository<TEntity, TId01, TId02, TId03>, ISyncRepository<TEntity, TId01, TId02, TId03>
        where TEntity : class
    {

    }

    /// <summary>
    /// Repository interface that will be used to query and manipulate
    /// persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    /// <typeparam name="TId03">The entity id third type</typeparam>
    /// <typeparam name="TId04">The entity id fourth type</typeparam>
    public interface IRepository<TEntity, in TId01, in TId02, in TId03, in TId04> : IAsyncRepository<TEntity, TId01, TId02, TId03, TId04>, ISyncRepository<TEntity, TId01, TId02, TId03, TId04>
        where TEntity : class
    {

    }

    /// <summary>
    /// Repository interface that will be used to query and manipulate
    /// persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IRepository<TEntity> : IAsyncRepository<TEntity>, ISyncRepository<TEntity>
        where TEntity : class
    {

    }

#endif
}

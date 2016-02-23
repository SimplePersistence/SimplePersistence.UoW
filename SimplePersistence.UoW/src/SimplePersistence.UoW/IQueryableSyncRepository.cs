namespace SimplePersistence.UoW
{
#if !NET20

    using System.Linq;

    /// <summary>
    /// Represents a repository that only exposes synchronous operations 
    /// to manipulate persisted entities and can be used as an <see cref="IQueryable{T}"/> source
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The entity id type</typeparam>
#if !NET35
    public interface IQueryableSyncRepository<TEntity, in TId> 
        : ISyncRepository<TEntity, TId>, IExposeQueryable<TEntity, TId>
        where TEntity : class
#else
    public interface IQueryableRepository<TEntity, in TId>
        : IRepository<TEntity, TId>, IExposeQueryable<TEntity, TId>
        where TEntity : class
#endif
    {

    }

    /// <summary>
    /// Represents a repository that only exposes synchronous operations 
    /// to manipulate persisted entities and can be used as an <see cref="IQueryable{T}"/> source
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
#if !NET35
    public interface IQueryableSyncRepository<TEntity, in TId01, in TId02> 
        : ISyncRepository<TEntity, TId01, TId02>, IExposeQueryable<TEntity, TId01, TId02>
        where TEntity : class
#else
    public interface IQueryableRepository<TEntity, in TId01, in TId02>
        : IRepository<TEntity, TId01, TId02>, IExposeQueryable<TEntity, TId01, TId02>
        where TEntity : class
#endif
    {

    }

    /// <summary>
    /// Represents a repository that only exposes synchronous operations 
    /// to manipulate persisted entities and can be used as an <see cref="IQueryable{T}"/> source
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    /// <typeparam name="TId03">The entity id third type</typeparam>
#if !NET35
    public interface IQueryableSyncRepository<TEntity, in TId01, in TId02, in TId03> 
        : ISyncRepository<TEntity, TId01, TId02, TId03>, IExposeQueryable<TEntity, TId01, TId02, TId03>
        where TEntity : class
#else
    public interface IQueryableRepository<TEntity, in TId01, in TId02, in TId03>
        : IRepository<TEntity, TId01, TId02, TId03>, IExposeQueryable<TEntity, TId01, TId02, TId03>
        where TEntity : class
#endif
    {

    }

    /// <summary>
    /// Represents a repository that only exposes synchronous operations 
    /// to manipulate persisted entities and can be used as an <see cref="IQueryable{T}"/> source
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    /// <typeparam name="TId03">The entity id third type</typeparam>
    /// <typeparam name="TId04">The entity id fourth type</typeparam>
#if !NET35
    public interface IQueryableSyncRepository<TEntity, in TId01, in TId02, in TId03, in TId04> 
        : ISyncRepository<TEntity, TId01, TId02, TId03, TId04>, IExposeQueryable<TEntity, TId01, TId02, TId03, TId04>
        where TEntity : class
#else
    public interface IQueryableRepository<TEntity, in TId01, in TId02, in TId03, in TId04>
        : IRepository<TEntity, TId01, TId02, TId03, TId04>, IExposeQueryable<TEntity, TId01, TId02, TId03, TId04>
        where TEntity : class
#endif
    {

    }

    /// <summary>
    /// Represents a repository that only exposes synchronous operations 
    /// to manipulate persisted entities and can be used as an <see cref="IQueryable{T}"/> source
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
#if !NET35
    public interface IQueryableSyncRepository<TEntity> 
        : ISyncRepository<TEntity>, IExposeQueryable<TEntity>
        where TEntity : class
#else
    public interface IQueryableRepository<TEntity>
        : IRepository<TEntity>, IExposeQueryable<TEntity>
        where TEntity : class
#endif
    {

    }

#endif
}

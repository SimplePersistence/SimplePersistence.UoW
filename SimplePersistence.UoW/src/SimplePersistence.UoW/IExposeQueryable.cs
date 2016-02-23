namespace SimplePersistence.UoW
{
#if !(NET20)

    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Offers the possibility of exposing an <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The entity id type</typeparam>
    public interface IExposeQueryable<TEntity, in TId> 
        where TEntity : class
    {
        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> filtered by
        /// the entity id
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryById(TId id);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> 
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch);
    }

    /// <summary>
    /// Offers the possibility of exposing an <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    public interface IExposeQueryable<TEntity, in TId01, in TId02>
        where TEntity : class
    {
        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> filtered by
        /// the entity id
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryById(TId01 id01, TId02 id02);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> 
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch);
    }

    /// <summary>
    /// Offers the possibility of exposing an <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    /// <typeparam name="TId03">The entity id third type</typeparam>
    public interface IExposeQueryable<TEntity, in TId01, in TId02, in TId03>
        where TEntity : class
    {
        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> filtered by
        /// the entity id
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third unique identifier value</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryById(TId01 id01, TId02 id02, TId03 id03);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> 
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch);
    }

    /// <summary>
    /// Offers the possibility of exposing an <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    /// <typeparam name="TId03">The entity id third type</typeparam>
    /// <typeparam name="TId04">The entity id fourth type</typeparam>
    public interface IExposeQueryable<TEntity, in TId01, in TId02, in TId03, in TId04>
        where TEntity : class
    {
        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> filtered by
        /// the entity id
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third unique identifier value</param>
        /// <param name="id04">The entity fourth unique identifier value</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryById(TId01 id01, TId02 id02, TId03 id03, TId04 id04);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> 
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch);
    }

    /// <summary>
    /// Offers the possibility of exposing an <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IExposeQueryable<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> filtered by
        /// the entity id
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryById(params object[] ids);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> 
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch);
    }

#endif
}

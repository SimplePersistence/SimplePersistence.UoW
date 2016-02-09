﻿namespace SimplePersistence.UoW
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a repository that only exposes synchronous operations
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The entity id type</typeparam>
    public interface ISyncRepository<TEntity, in TId>
        where TEntity : class 
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>The entity or null if not found</returns>
        TEntity GetById(TId id);

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        IEnumerable<TEntity> Add(IEnumerable<TEntity> entities);

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        IEnumerable<TEntity> Add(params TEntity[] entities);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The entity</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Update(params TEntity[] entities);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>The entity</returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Delete(params TEntity[] entities);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>The total</returns>
        long Total();

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>True if entity exists</returns>
        bool Exists(TId id);

        #endregion
    }

    /// <summary>
    /// Represents a repository that only exposes synchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    public interface ISyncRepository<TEntity, in TId01, in TId02>
        where TEntity : class
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>The entity or null if not found</returns>
        TEntity GetById(TId01 id01, TId02 id02);

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        IEnumerable<TEntity> Add(IEnumerable<TEntity> entities);

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        IEnumerable<TEntity> Add(params TEntity[] entities);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The entity</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Update(params TEntity[] entities);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>The entity</returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Delete(params TEntity[] entities);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>The total</returns>
        long Total();

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>True if entity exists</returns>
        bool Exists(TId01 id01, TId02 id02);

        #endregion
    }

    /// <summary>
    /// Represents a repository that only exposes synchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    /// <typeparam name="TId03">The entity id third type</typeparam>
    public interface ISyncRepository<TEntity, in TId01, in TId02, in TId03>
        where TEntity : class
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <returns>The entity or null if not found</returns>
        TEntity GetById(TId01 id01, TId02 id02, TId03 id03);

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        IEnumerable<TEntity> Add(IEnumerable<TEntity> entities);

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        IEnumerable<TEntity> Add(params TEntity[] entities);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The entity</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Update(params TEntity[] entities);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>The entity</returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Delete(params TEntity[] entities);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>The total</returns>
        long Total();

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <returns>True if entity exists</returns>
        bool Exists(TId01 id01, TId02 id02, TId03 id03);

        #endregion
    }

    /// <summary>
    /// Represents a repository that only exposes synchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    /// <typeparam name="TId03">The entity id third type</typeparam>
    /// <typeparam name="TId04">The entity id fourth type</typeparam>
    public interface ISyncRepository<TEntity, in TId01, in TId02, in TId03, in TId04>
        where TEntity : class
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="id04">The entity fourth identifier value</param>
        /// <returns>The entity or null if not found</returns>
        TEntity GetById(TId01 id01, TId02 id02, TId03 id03, TId04 id04);

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        IEnumerable<TEntity> Add(IEnumerable<TEntity> entities);

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        IEnumerable<TEntity> Add(params TEntity[] entities);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The entity</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Update(params TEntity[] entities);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>The entity</returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Delete(params TEntity[] entities);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>The total</returns>
        long Total();

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="id04">The entity fourth identifier value</param>
        /// <returns>True if entity exists</returns>
        bool Exists(TId01 id01, TId02 id02, TId03 id03, TId04 id04);

        #endregion
    }

    /// <summary>
    /// Represents a repository that only exposes synchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface ISyncRepository<TEntity>
        where TEntity : class
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>The entity or null if not found</returns>
        TEntity GetById(params object[] ids);

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        IEnumerable<TEntity> Add(IEnumerable<TEntity> entities);

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The range of entities added</returns>
        IEnumerable<TEntity> Add(params TEntity[] entities);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The entity</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Update(params TEntity[] entities);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>The entity</returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The entities</returns>
        IEnumerable<TEntity> Delete(params TEntity[] entities);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>The total</returns>
        long Total();

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>True if entity exists</returns>
        bool Exists(params object[] ids);

        #endregion
    }
}

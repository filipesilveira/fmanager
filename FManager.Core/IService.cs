namespace FManager.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IService<TEntity>
    {
        /// <summary>
        /// Obtain the async list of generic Entities
        /// </summary>
        /// <returns>The async list of generic Entities</returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Obtain the async queryable of generic Entities
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetAllQueryableAsync();

        /// <summary>
        /// Obtain the async queryable of generic Entities as not tracking
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetAllQueryableNotTrackingAsync();

        /// <summary>
        ///  Obtain the async list of generic Entities by criteria
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllByCriteriaAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///  Obtain the async queryable of generic Entities by criteria
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetAllQueryableByCriteriaAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetAllQueryableByCriteriaNotTrackingAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Obtain the async list of generic Entities by a criteria
        /// </summary>
        /// <param name="predicate">The criteria</param>
        /// <returns>The async list of generic Entities</returns>
        Task<TEntity> GetByCriteriaAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> GetByCriteriaNotTrackingAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Add a new Entity into the repository and do a commit
        /// </summary>
        /// <param name="entity">The generic Entity</param>
        /// <returns>The new Entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Add a new Entity async into the repository and do a commit
        /// </summary>
        /// <param name="entity">The generic Entity</param>
        /// <returns>The new Entity</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Update an Entity into the repository and do a commit
        /// </summary>
        /// <param name="entity">The generic Entity</param>
        /// <returns>The updated Entity</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Update an Entity by an not Tracked TEntity into the repository and do a commit
        /// </summary>
        /// <param name="entity">The generic Entity</param>
        /// <param name="notTrackedEntity">not mapped(tracked) TEntity</param>
        /// <returns>The updated Entity</returns>
        TEntity DirtyUpdate(TEntity entity, TEntity notTrackedEntity);


        /// <summary>
        /// Update an Entity async into the repository and do a commit
        /// </summary>
        /// <param name="entity">The generic Entity</param>
        /// <returns>The updated Entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete an Entity into the repository and do a commit
        /// </summary>
        /// <param name="entity">The generic Entity</param>
        /// <returns>The deleted Entity</returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// Delete an Entity async into the repository and do a commit
        /// </summary>
        /// <param name="entity">The generic Entity</param>
        /// <returns>The deleted Entity</returns>
        Task<TEntity> DeleteAsync(TEntity entity);

        /// <summary>
        /// Obtain the Entity using cache
        /// </summary>
        /// <returns>Entity by id</returns>
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
    }
}

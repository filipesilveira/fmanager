namespace FManager.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Obtain the async list of Entities
        /// </summary>
        /// <returns>Async list of Entities</returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetAllQueryableAsync();

        Task<IQueryable<TEntity>> GetAllQueryableNotTrackingAsync();

        /// <summary>
        /// Obtain the async list of Entities
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllByCriteriaAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///
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
        /// Obtain the async list of Entities by criteria
        /// </summary>
        /// <param name="predicate">A criteria</param>
        /// <returns>Async list of Entities by criteria</returns>
        Task<TEntity> GetByCriteriaAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> GetByCriteriaNotTrackingAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Add a new Entity into the context
        /// </summary>
        /// <param name="entity">>The new Entity</param>
        void Add(TEntity entity);

        /// <summary>
        /// Update an Entity
        /// </summary>
        /// <param name="entity">The Entity</param>
        void Update(TEntity entity);

        /// <summary>
        /// Update an Entity not checking for attached
        /// </summary>
        /// <param name="entity">The Entity</param>
        void DirtyUpdate(TEntity entity, object dto);

        /// <summary>
        /// Delete an Entity
        /// </summary>
        /// <param name="entity">The Entity</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Obtain the Entity using cache
        /// </summary>
        /// <returns>Entity by id</returns>
        TEntity Find(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
    }
}

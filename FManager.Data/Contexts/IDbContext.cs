namespace FManager.Data.Contexts
{
    using FManager.Data.Entities.Shared;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// This interface define an abstract DbContext
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// Add an Entity
        /// </summary>
        /// <typeparam name="TEntity">The generic Entity</typeparam>
        /// <param name="entity">The Entity</param>
        void SetAsAdded<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// Update an Entity
        /// </summary>
        /// <typeparam name="TEntity">The generic Entity</typeparam>
        /// <param name="entity">The Entity</param>
        /// <param name="trusted">Define if call attach if not attached</param>
        void SetAsModified<TEntity>(TEntity entity, bool trusted = true) where TEntity : BaseEntity;
        
        /// <summary>
        /// Delete an Entity
        /// </summary>
        /// <typeparam name="TEntity">The generic Entity</typeparam>
        /// <param name="entity">The Entity</param>
        void SetAsDeleted<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// Set the entity as dirty
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="dto"></param>
        void DirtyUpdate<TEntity>(TEntity entity, object dto) where TEntity : BaseEntity;

        /// <summary>
        /// Obtain a list of async generic Entities
        /// </summary>
        /// <typeparam name="TEntity">The generic Entity</typeparam>
        /// <returns>A list of async generic Entities</returns>
        Task<List<TEntity>> ToListAsync<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Obtain a list of async generic Entities by criteria
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        Task<List<TEntity>> ToListByCriteriaAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;

        /// <summary>
        /// Obtain a list query of async generic Entities
        /// </summary>
        /// <typeparam name="TEntity">The generic Entity</typeparam>
        /// <returns>A list query of async generic Entities</returns>
        Task<IQueryable<TEntity>> ToQueryableAsync<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Obtain a list query of async generic Entities with not tracking
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        Task<IQueryable<TEntity>> ToQueryableNotTrackingAsync<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Obtain a list query of async generic Entities by criteria
        /// </summary>
        /// <typeparam name="TEntity">The generic Entity</typeparam>
        /// <returns>A list query of async generic Entities</returns>
        Task<IQueryable<TEntity>> ToQueryableByCriteriaAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;

        /// <summary>
        /// Obtain a list query of async generic Entities by criteria with not tracking
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> ToQueryableByCriteriaNotTrackingAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;

        /// <summary>
        /// Obtain first or something async generic Entity
        /// </summary>
        /// <typeparam name="TEntity">The generic Entity</typeparam>
        /// <param name="predicate">A criteria</param>
        /// <returns>An Entity or default</returns>
        Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;

        /// <summary>
        /// Obtain first or null with not tracking
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultNotTrackingAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;

        /// <summary>
        /// Obtain the entity by Id
        /// </summary>
        /// <typeparam name="TEntity">The generic Entity</typeparam>
        /// <returns>An Entity</returns>
        TEntity Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;

        /// <summary>
        /// Obtain a list of entities
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;

        /// <summary>
        /// Begin an transaction
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Do a commit
        /// </summary>
        /// <returns>An identifier value</returns>
        int Commit();

        /// <summary>
        ///  Do an async commit
        /// </summary>
        /// <returns>An identifier value</returns>
        Task<int> CommitAsync();

        /// <summary>
        /// Do a rollback
        /// </summary>
        void Rollback();
               
        /// <summary>
        /// The database name.
        /// </summary>
        /// <returns></returns>
        string DatabaseName();

        /// <summary>
        /// Set the lazy loading
        /// </summary>
        /// <param name="active"></param>
        void SwitchProxy(bool active);

        /// <summary>
        /// The list of entities on the DBContext
        /// </summary>
        /// <returns></returns>
        List<string> EntityNames();        
    }
}

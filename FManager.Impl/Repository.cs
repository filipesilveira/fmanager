namespace FManager.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Core;
    using FManager.Data.Contexts;
    using FManager.Data.Entities.Shared;

    /// <summary>
    /// Repository implementation with basic generic operations to handle {TEntity}
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// The DataBase Context
        /// </summary>
        protected readonly IDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}" /> class
        /// </summary>
        /// <param name="context">The implementation of Database Context <see cref="IDbContext" /></param>
        public Repository(IDbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public Task<List<TEntity>> GetAllAsync()
        {
            return this.context.ToListAsync<TEntity>();
        }

        /// <inheritdoc/>
        public Task<IQueryable<TEntity>> GetAllQueryableAsync()
        {
            return this.context.ToQueryableAsync<TEntity>();
        }

        /// <inheritdoc/>
        public Task<IQueryable<TEntity>> GetAllQueryableNotTrackingAsync()
        {
            return this.context.ToQueryableNotTrackingAsync<TEntity>();
        }

        /// <inheritdoc/>
        public Task<IQueryable<TEntity>> GetAllQueryableByCriteriaAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.context.ToQueryableByCriteriaAsync<TEntity>(predicate);
        }

        /// <inheritdoc/>
        public Task<IQueryable<TEntity>> GetAllQueryableByCriteriaNotTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.context.ToQueryableByCriteriaNotTrackingAsync<TEntity>(predicate);
        }

        /// <inheritdoc/>
        public Task<List<TEntity>> GetAllByCriteriaAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.context.ToListByCriteriaAsync<TEntity>(predicate);
        }

        /// <inheritdoc/>
        public Task<TEntity> GetByCriteriaAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.context.FirstOrDefaultAsync<TEntity>(predicate);
        }

        /// <inheritdoc/>
        public Task<TEntity> GetByCriteriaNotTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.context.FirstOrDefaultNotTrackingAsync<TEntity>(predicate);
        }

        /// <inheritdoc/>
        public void Add(TEntity entity)
        {
            this.context.SetAsAdded<TEntity>(entity);
        }

        /// <inheritdoc/>
        public void Update(TEntity entity)
        {
            this.context.SetAsModified<TEntity>(entity);
        }

        /// <inheritdoc/>
        public void DirtyUpdate(TEntity entity, object dto)
        {
            this.context.DirtyUpdate(entity, dto);
        }

        /// <inheritdoc/>
        public void Delete(TEntity entity)
        {
            this.context.SetAsDeleted<TEntity>(entity);
        }

        /// <inheritdoc/>
        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.context.Find(predicate);
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return this.context.FindAll(predicate);
        }
    }
}

// <copyright file="Service.cs" company="FS Tecnology">
// Copyright (c) FS Tecnology. All rights reserved.
// </copyright>

namespace FManager.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Core;
    using FManager.Data.Entities.Shared;

    /// <summary>
    /// Base service class to handle basic operations related to specified Entity
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public class Service<TEntity> : IService<TEntity>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Service{TEntity}" /> class
        /// </summary>
        /// <param name="unitOfWork">The implementation of Unit Of Work pattern <see cref="IUnitOfWork" /></param>
        public Service(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
            this.Repository = this.UnitOfWork.Repository<TEntity>();
        }

        /// <summary>
        /// Gets or Sets the Unit Of Work pattern <see cref="IUnitOfWork" />
        /// </summary>
        public IUnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        /// Gets or sets repository instance used to perform database actions
        /// </summary>
        protected IRepository<TEntity> Repository { get; set; }

        /// <inheritdoc/>
        public Task<List<TEntity>> GetAllAsync()
        {
            return this.Repository.GetAllAsync();
        }

        /// <inheritdoc/>
        public Task<IQueryable<TEntity>> GetAllQueryableAsync()
        {
            return this.Repository.GetAllQueryableAsync();
        }

        /// <inheritdoc/>
        public Task<IQueryable<TEntity>> GetAllQueryableNotTrackingAsync()
        {
            return this.Repository.GetAllQueryableNotTrackingAsync();
        }

        /// <inheritdoc/>
        public Task<IQueryable<TEntity>> GetAllQueryableByCriteriaAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Repository.GetAllQueryableByCriteriaAsync(predicate);
        }

        /// <inheritdoc/>
        public Task<IQueryable<TEntity>> GetAllQueryableByCriteriaNotTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Repository.GetAllQueryableByCriteriaNotTrackingAsync(predicate);
        }

        /// <inheritdoc/>
        public Task<List<TEntity>> GetAllByCriteriaAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Repository.GetAllByCriteriaAsync(predicate);
        }

        /// <inheritdoc/>
        public virtual Task<TEntity> GetByCriteriaAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Repository.GetByCriteriaAsync(predicate);
        }

        /// <inheritdoc/>
        public virtual Task<TEntity> GetByCriteriaNotTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Repository.GetByCriteriaNotTrackingAsync(predicate);
        }

        /// <inheritdoc/>
        public TEntity Add(TEntity entity)
        {
            this.Repository.Add(entity);
            this.UnitOfWork.Commit();

            return entity;
        }

        /// <inheritdoc/>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            this.Repository.Add(entity);
            await this.UnitOfWork.CommitAsync();

            return entity;
        }

        /// <inheritdoc/>
        public TEntity Update(TEntity entity)
        {
            this.Repository.Update(entity);
            this.UnitOfWork.Commit();

            return entity;
        }

        /// <inheritdoc/>
        public TEntity DirtyUpdate(TEntity entity, TEntity dto)
        {
            this.Repository.DirtyUpdate(entity, dto);
            return entity;
        }

        /// <inheritdoc/>
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            this.Repository.Update(entity);
            await this.UnitOfWork.CommitAsync();

            return entity;
        }

        /// <inheritdoc/>
        public TEntity Delete(TEntity entity)
        {
            this.Repository.Delete(entity);
            this.UnitOfWork.Commit();

            return entity;
        }

        /// <inheritdoc/>
        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            this.Repository.Delete(entity);
            await this.UnitOfWork.CommitAsync();

            return entity;
        }

        /// <inheritdoc/>
        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Repository.Find(predicate);
        }
    }
}

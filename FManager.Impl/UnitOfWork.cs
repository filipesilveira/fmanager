// <copyright file="UnitOfWork.cs" company="FS Tecnology">
// Copyright (c) FS Tecnology. All rights reserved.
// </copyright>

namespace FManager.Impl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using FManager.Core.Helpers;
    using FManager.Data.Contexts;
    using FManager.Data.Entities.Shared;

    /// <summary>
    /// Responsible for handling repositories, DbContexts,
    /// transactions and database connection
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// A hash of repositories
        /// </summary>
        private Hashtable repositories;

        /// <summary>
        /// A pool of DBContext
        /// </summary>
        private IEnumerable<IDbContext> contexts;

        /// <summary>
        /// Gets or sets a cache mapping {Entity, DBContext}
        /// </summary>
        private Dictionary<string, IDbContext> ContextsMap { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class
        /// </summary>
        /// <param name="contexts">The List of implementation of DBContext <see cref="IDbContext" /></param>
        public UnitOfWork(IEnumerable<IDbContext> contexts)
        {
            if (contexts == null || contexts.Count() == 0)
            {
                throw new Exception("We need at least one DBContext implementation.");
            }

            this.contexts = contexts;
            this.FillContextsMap(this.contexts);
        }

        /// <inheritdoc/>
        public void BeginTransaction()
        {
            this.contexts.ForEach((c) => c.BeginTransaction());
        }

        /// <inheritdoc/>
        public int Commit()
        {
            return this.contexts.Sum((c) => c.Commit());
        }

        /// <inheritdoc/>
        public async Task<int> CommitAsync()
        {
            int count = 0;
            foreach (var context in this.contexts)
            {
                count += await context.CommitAsync();
            }

            return count;
        }

        /// <inheritdoc/>
        public IRepository<TEntity> Repository<TEntity>()
            where TEntity : BaseEntity
        {
            if (this.repositories == null)
            {
                this.repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (this.repositories.ContainsKey(type))
            {
                return (IRepository<TEntity>)this.repositories[type];
            }

            var repositoryType = typeof(Repository<TEntity>);

            if (!ContextsMap.ContainsKey(type))
            {
                throw new Exception($"We need to register {type} in EntityNames method implemented by the DBContext");
            }

            IDbContext currentContext = ContextsMap[type];
            this.repositories.Add(type, Activator.CreateInstance(repositoryType, currentContext));

            return (IRepository<TEntity>)this.repositories[type];
        }

        /// <inheritdoc/>
        public void Rollback()
        {
            this.contexts.ForEach((c) => c.Rollback());
        }

        /// <inheritdoc/>
        public void SwitchProxy(bool active)
        {
            this.contexts.ForEach((c) => c.SwitchProxy(active));
        }

        /// <inheritdoc/>
        public string DatabaseName()
        {
            return this.contexts.First().DatabaseName();
        }

        /// <summary>
        /// This method fill the cache mapping with the entities name and the IDbContext belong
        /// </summary>
        /// <param name="contexts">The List of implementation of DBContext <see cref="IDbContext" /></param>
        private void FillContextsMap(IEnumerable<IDbContext> contexts)
        {
            this.ContextsMap = new Dictionary<string, IDbContext>();
            foreach (var context in contexts)
            {
                List<string> entityNames = context.EntityNames();
                foreach (var name in entityNames)
                {
                    this.ContextsMap.Add(name, context);
                }
            }
        }
    }
}

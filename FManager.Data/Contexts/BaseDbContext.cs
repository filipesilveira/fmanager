using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FManager.Data.Entities.DayBooks;
using FManager.Data.Entities.ManageBO;
using FManager.Data.Entities.Shared;
using FManager.Data.Entities.Users;

namespace FManager.Data.Contexts
{
    public abstract class BaseDbContext : DbContext, IDbContext
    {
        /// <summary>
        /// An Object Context
        /// </summary>
        private ObjectContext objectContext;

        /// <summary>
        /// A transaction Object
        /// </summary>
        private DbTransaction transaction;

        public BaseDbContext()
            : base("name=Dev")
        {
            Database.SetInitializer<FManagerEntities>(null);
            Database.SetInitializer<BaseDbContext>(null);

            this.Database.CommandTimeout = 300; // 5 minutes.
        }

        public BaseDbContext(string conName) : base($"name={conName}") //Used in tests project
        {
            Database.SetInitializer<FManagerEntities>(null);
            Database.SetInitializer<BaseDbContext>(null);

            this.Database.CommandTimeout = 300; // 5 minutes.
        }

        public BaseDbContext(DbConnection connection) : base(connection, true)
        {
            this.Database.CommandTimeout = 300; // 5 minutes.
        }


        #region IDbContext Implementation

        /// <inheritdoc/>
        public void BeginTransaction()
        {
            this.objectContext = ((IObjectContextAdapter)this).ObjectContext;
            if (this.objectContext.Connection.State == ConnectionState.Open)
            {
                return;
            }

            this.objectContext.Connection.Open();
            this.transaction = this.objectContext.Connection.BeginTransaction();
        }

        /// <inheritdoc/>
        public int Commit()
        {
            try
            {
                this.BeginTransaction();
                var saveChanges = this.SaveChanges();
                this.EndTransaction();

                return saveChanges;
            }
            catch (Exception)
            {
                this.Rollback();
                throw;
            }
            finally
            {
            }
        }

        /// <inheritdoc/>
        public async Task<int> CommitAsync()
        {
            try
            {
                this.BeginTransaction();
                var saveChangesAsync = await this.SaveChangesAsync();
                this.EndTransaction();

                return saveChangesAsync;
            }
            catch
            {
                this.Rollback();
                throw;
            }
            finally
            {
            }
        }

        private void EndTransaction()
        {
            this.transaction.Commit();
            this.objectContext.Connection.Close();
            this.transaction.Dispose();
        }

        /// <inheritdoc/>
        public Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : BaseEntity
        {
            return this.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        /// <inheritdoc/>
        public Task<TEntity> FirstOrDefaultNotTrackingAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : BaseEntity
        {
            return this.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        /// <inheritdoc/>
        public void Rollback()
        {
            if (this.transaction?.Connection != null)
            {
                this.transaction.Rollback();
            }
        }

        /// <inheritdoc/>
        public void SetAsAdded<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            this.UpdateEntityState<TEntity>(entity, EntityState.Added);
        }

        /// <inheritdoc/>
        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            this.UpdateEntityState<TEntity>(entity, EntityState.Deleted);
        }

        /// <inheritdoc/>
        public void SetAsModified<TEntity>(TEntity entity, bool trusted = true) where TEntity : BaseEntity
        {
            this.UpdateEntityState<TEntity>(entity, EntityState.Modified, trusted);
        }

        /// <inheritdoc/>
        public void DirtyUpdate<TEntity>(TEntity entity, object dto) where TEntity : BaseEntity
        {
            this.Entry(entity).CurrentValues.SetValues(dto);
        }

        /// <inheritdoc/>
        public Task<List<TEntity>> ToListAsync<TEntity>() where TEntity : BaseEntity
        {
            return this.Set<TEntity>().ToListAsync<TEntity>();
        }

        /// <inheritdoc/>
        public Task<List<TEntity>> ToListByCriteriaAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : BaseEntity
        {
            return this.Set<TEntity>().Where(predicate).ToListAsync<TEntity>();
        }

        /// <inheritdoc/>
        public Task<IQueryable<TEntity>> ToQueryableByCriteriaAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : BaseEntity
        {
            return Task.FromResult(this.Set<TEntity>().Where(predicate).AsQueryable());
        }

        /// <inheritdoc/>
        public Task<IQueryable<TEntity>> ToQueryableByCriteriaNotTrackingAsync<TEntity>(
            Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            return Task.FromResult(this.Set<TEntity>().Where(predicate).AsNoTracking().AsQueryable());
        }

        /// <inheritdoc/>
        public Task<IQueryable<TEntity>> ToQueryableAsync<TEntity>() where TEntity : BaseEntity
        {
            return Task.FromResult(this.Set<TEntity>().AsQueryable());
        }

        /// <inheritdoc/>
        public Task<IQueryable<TEntity>> ToQueryableNotTrackingAsync<TEntity>() where TEntity : BaseEntity
        {
            return Task.FromResult(this.Set<TEntity>().AsQueryable().AsNoTracking());
        }

        /// <inheritdoc/>
        public TEntity Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            return FindAll(predicate).FirstOrDefault();
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> FindAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            var final = this.Set<TEntity>()
                .Where(predicate)
                .ToList();

            final.AddRange(ChangeTracker.Entries()
                .Where(a => a.Entity is TEntity)
                .Select(a => a.Entity as TEntity)
                .AsQueryable()
                .Where(predicate)
                .ToList());

            return final;
        }

        /// <inheritdoc/>
        public void SwitchProxy(bool active)
        {
            // avoid in some cases lazy loading see https://ardalis.com/avoid-lazy-loading-entities-in-asp-net-applications
            this.Configuration.ProxyCreationEnabled = active;
            this.Configuration.LazyLoadingEnabled = active;
        }

        /// <inheritdoc/>
        public string DatabaseName()
        {
            return this.Database.Connection.Database;
        }

        /// <inheritdoc/>
        public List<string> EntityNames()
        {
            // Is better if we put here all the entities manually to avoid need run migration in the first step loading the app.
            // Ex. return new List<string> { "MyEntity", "MyEntityTwo", "MyEntityThree" };

            ObjectContext objContext = ((IObjectContextAdapter)this).ObjectContext;
            MetadataWorkspace workspace = objContext.MetadataWorkspace;
            IEnumerable<EntityType> tables = workspace.GetItems<EntityType>(DataSpace.SSpace);

            return new List<string>() {
                 nameof(BaseEntity)
                ,nameof(BaseEntityWithUser)
                ,nameof(PaymentPlan)
                ,nameof(PaymentHistory)
                ,nameof(PaymentStatus)
                ,nameof(User)
                ,nameof(DayBook)
                ,nameof(DayBookItem)
                ,nameof(Account)
                ,nameof(Currency)
                ,nameof(Parity)
                ,nameof(Section)
                ,nameof(Entities.ManageBO.Entry)
            };
        }

        private void UpdateEntityState<TEntity>(TEntity entity, EntityState entityState, bool trusted = true) where TEntity : BaseEntity
        {
            var entityEntry = this.GetDbEntityEntrySafely<TEntity>(entity, trusted);
            if (entityEntry.State == EntityState.Unchanged || !trusted)
            {
                entityEntry.State = entityState;
            }
        }

        private DbEntityEntry GetDbEntityEntrySafely<TEntity>(TEntity entity, bool attachIfNeeded = true) where TEntity : BaseEntity
        {
            var entityEntry = this.Entry<TEntity>(entity);
            if (entityEntry.State == EntityState.Detached && attachIfNeeded)
            {
                this.Set<TEntity>().Attach(entity);
            }

            return entityEntry;
        }


        public List<T> ExecStoreProcedure<T>(string spName, IEnumerable<SqlParameter> sqlParams)
        {
            var sp = spName;
            foreach (var param in sqlParams)
            {
                if (sp != spName)
                {
                    sp += ",";
                }
                sp += " @" + param.ParameterName;
            }
            return this.Database.SqlQuery<T>(sp, sqlParams.ToArray()).ToList();
        }
        #endregion IDbContext Implementation

    }
}
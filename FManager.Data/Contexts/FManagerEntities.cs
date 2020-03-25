namespace FManager.Data.Contexts
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Diagnostics;
    using FManager.Data.Entities.DayBooks;
    using FManager.Data.Entities.ManageBO;

    public class FManagerEntities : BaseDbContext
    {
        public FManagerEntities() : base()
        {
#if DEBUG
            Database.Log = s => Debug.Print(s);
#endif
        }
        public FManagerEntities(string conName) : base(conName) { }
        public FManagerEntities(DbConnection connection) : base(connection) { }

        /// <inheritdoc/>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        private string getTableNameAttribute(Type t)
        {
            var attr = (TableAttribute)Attribute.GetCustomAttribute(t, typeof(TableAttribute));
            if (attr != null)
            {
                return attr.Name;
            }
            else
            {
                return null;
            }
        }

        #region Tables

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Entry> Entries { get; set; }
        public virtual DbSet<Parity> Parities { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<DayBook> DayBooks { get; set; }
        public virtual DbSet<DayBookItem> DayBookItems { get; set; }

        #endregion
    }
}
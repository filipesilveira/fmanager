using System;

namespace FManager.Data.Entities.Shared
{
    public abstract class BaseEntity
    {
        public bool Deleted { get; set; }
        public DateTime Change { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
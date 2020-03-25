using FManager.Data.Entities.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FManager.Data.Entities.Shared
{
    public abstract class BaseEntityWithUser : BaseEntity
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
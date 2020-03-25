using FManager.Data.Entities.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FManager.Data.Entities.Users
{
    public class User : BaseEntity
    {
        [Key]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid SessionToken { get; set; }
        public virtual ICollection<PaymentHistory> PaymentHistories { get; set; }
    }
}
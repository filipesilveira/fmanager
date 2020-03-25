using FManager.Data.Entities.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace FManager.Data.Entities.Users
{
    public class PaymentStatus : BaseEntity
    {
        [Key]
        public Guid PaymentStatusId { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
    }
}
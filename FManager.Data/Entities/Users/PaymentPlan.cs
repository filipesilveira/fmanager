using System;
using System.ComponentModel.DataAnnotations;

namespace FManager.Data.Entities.Shared
{
    public class PaymentPlan : BaseEntity
    {
        [Key]
        public Guid PaymentPlanId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public int Days { get; set; }
    }
}
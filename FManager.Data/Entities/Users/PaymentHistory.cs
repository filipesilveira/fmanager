using FManager.Data.Entities.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FManager.Data.Entities.Users
{
    public class PaymentHistory : BaseEntityWithUser
    {
        [Key]
        public Guid PaymentHistoryId { get; set; }
        public string Reference { get; set; }
        public DateTime? Expiration { get; set; }
        public string Code { get; set; }

        [ForeignKey("PaymentStatus")]
        public Guid PaymentStatusId { get; set; }
        public virtual PaymentStatus PaymentStatus { get; set; }

        [ForeignKey("PaymentPlan")]
        public Guid PaymentPlanId { get; set; }
        public virtual PaymentPlan PaymentPlan { get; set; }
    }
}
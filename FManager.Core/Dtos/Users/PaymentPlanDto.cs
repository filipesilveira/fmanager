using System;

namespace FManager.Core.Dtos.Users
{
    public class PaymentPlanDto
    {
        public Guid PaymentPlanId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public int Days { get; set; }
    }
}

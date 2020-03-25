namespace FManager.Core.Models.ManageBO
{
    using System;

    public class ReviewModel
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DayOfWeek? DayOfWeek { get; set; }

        public Guid? AccountId { get; set; }

        public bool Operations { get; set; }

        public int Page { get; set; }

        public int Count { get; set; }
    }
}

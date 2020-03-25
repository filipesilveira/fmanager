namespace FManager.Core.Models.ManageBO
{
    using System;

    public class AssertivenessModel
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Guid? ParityId { get; set; }

        public DayOfWeek? DayOfWeek { get; set; }

        public Guid? AccountId { get; set; }
    }
}

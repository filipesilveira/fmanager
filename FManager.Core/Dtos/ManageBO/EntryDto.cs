namespace FManager.Core.Dtos.ManageBO
{
    using FManager.Core.Dtos.Shared;
    using Newtonsoft.Json;
    using System;
    using System.Linq;

    public class EntryDto : BaseEntityDto
    {
        public Guid EntryId { get; set; }
        public DateTime DateAndTime { get; set; }
        public decimal Value { get; set; }
        public decimal Payout { get; set; }
        public bool Win { get; set; }
        public string Summary { get; set; }
        public Guid ParityId { get; set; }
        public string ParityName { get; set; }
        public Guid SectionId { get; set; }

        [JsonIgnore]
        public SectionDto Section { get; set; }

        public decimal Balance {
            get {
                if (Section == null)
                {
                    return 0;
                }

                var balance = Section.Balance;

                foreach (var entry in Section.Entries.Where(w => w.DateAndTime < DateAndTime && !w.Deleted))
                {
                    balance += entry.Result;
                }

                return balance + Result;
            }
            set { }
        }

        public decimal Result {
            get {
                if (Win)
                {
                    return (Payout / 100) * Value;
                }
                else
                {
                    return - Value;
                }
            }
            set { }
        }

        public decimal Percentage
        {
            get
            {
                if (Balance == 0)
                {
                    return 0;
                }

                return (Result * 100) / Balance;
            }
            set { }
        }
    }
}

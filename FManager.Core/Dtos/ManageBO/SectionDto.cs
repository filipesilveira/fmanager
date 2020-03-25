namespace FManager.Core.Dtos.ManageBO
{
    using FManager.Core.Dtos.Shared;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SectionDto : BaseEntityDto
    {
        public Guid SectionId { get; set; }
        public DateTime? DateAndTime { get; set; }
        public string Summary { get; set; }
        public Guid AccountId { get; set; }
        public string AccountBroker { get; set; }
        public int TotalEntries { get; set; }

        [JsonIgnore]
        public AccountDto Account { get; set; }

        [JsonIgnore]
        public List<EntryDto> Entries { get; set; } = new List<EntryDto>();

        public string CurrencyName
        {
            get
            {
                if (Account == null)
                {
                    return null;
                }

                return Account.CurrencyName;
            }
            set { }
        }

        public decimal AccountBalance
        {
            get
            {
                if (Account == null)
                {
                    return 0;
                }

                return Account.Balance;
            }
            set { }
        }

        public decimal Balance
        {
            get
            {
                if (Account == null)
                {
                    return 0;
                }

                decimal balance = Account.InitialInvestment;
                var sections = Account.Sections.Where(w => w.DateAndTime < DateAndTime && !w.Deleted);

                foreach (var section in sections)
                {
                    balance += section.Result;
                }

                return balance;
            }
            set { }
        }

        public decimal Result
        {
            get
            {
                decimal result = 0;

                foreach (var entry in Entries.Where(w => !w.Deleted))
                {
                    if (entry.Win)
                    {
                        result += (entry.Payout / 100) * entry.Value;
                    }
                    else
                    {
                        result -= entry.Value;
                    }
                }

                return result;
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

        public int TotalWin
        {
            get
            {
                return Entries.Where(w => w.Win && !w.Deleted).Count();
            }
            set { }
        }

        public int TotalLoss
        {
            get
            {
                return Entries.Where(w => !w.Win && !w.Deleted).Count();
            }
            set { }
        }
    }
}

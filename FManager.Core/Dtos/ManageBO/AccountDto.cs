namespace FManager.Core.Dtos.ManageBO
{
    using FManager.Core.Dtos.Shared;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AccountDto : BaseEntityWithUserDto
    {
        public Guid AccountId { get; set; }
        public string Broker { get; set; }
        public decimal InitialInvestment { get; set; }
        public Guid CurrencyId { get; set; }
        public string CurrencyName { get; set; }

        [JsonIgnore]
        public List<SectionDto> Sections { get; set; } = new List<SectionDto>();

        public decimal Balance
        {
            get
            {
                decimal total = InitialInvestment;

                foreach (var section in Sections.Where(w => !w.Deleted))
                {
                    total += section.Result;
                }

                return total;
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

                return ((Balance - InitialInvestment) * 100) / InitialInvestment;
            }
            set { }
        }
    }
}

namespace FManager.Core.Dtos.ManageBO
{
    using System;

    public class TradingHistoryDto
    {
        public Guid ParityId { get; set; }

        public string ParityName { get; set; }

        public DateTime DateAndTime { get; set; }

        public Guid AccountId { get; set; }

        public decimal Value { get; set; }

        public decimal Result { get; set; }

        public decimal Equity { get; set; }

        public string CurrencyName { get; set; }
    }
}

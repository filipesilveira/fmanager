namespace FManager.Core.Dtos.ManageBO
{
    using System;

    public class IQOptionEntryDto
    {
        public DateTime OpeningDateTime { get; set; }
        public string Asset { get; set; }
        public string Instrument { get; set; }
        public decimal Investments { get; set; }
        public decimal Equity { get; set; }
        public decimal TotalPnL { get; set; }
        public decimal? GrossPnL { get; set; }
        public Guid SectionId { get; set; }
    }
}

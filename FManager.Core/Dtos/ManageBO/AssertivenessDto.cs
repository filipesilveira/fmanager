namespace FManager.Core.Dtos.ManageBO
{
    using System;

    public class AssertivenessDto
    {
        public Guid ParityId { get; set; }

        public string ParityName { get; set; }

        public decimal Percentage { get; set; }

        public int TotalWin { get; set; }

        public int TotalLoss { get; set; }

        public int TotalEntries { get; set; }
    }
}

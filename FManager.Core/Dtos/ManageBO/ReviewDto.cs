namespace FManager.Core.Dtos.ManageBO
{
    using System;
    using System.Collections.Generic;

    public class ReviewDto
    {
        public DateTime? DateAndTime { get; set; }

        public string Summary { get; set; }

        public List<string> EntriesSummaries { get; set; } = new List<string>();
    }
}

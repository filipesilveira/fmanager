namespace FManager.Core.Dtos.DayBooks
{
    using FManager.Core.Dtos.Shared;
    using System;

    public class DayBookItemDto : BaseEntityWithUserDto
    {
        public Guid DayBookItemId { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public Guid DayBookId { get; set; }
    }
}

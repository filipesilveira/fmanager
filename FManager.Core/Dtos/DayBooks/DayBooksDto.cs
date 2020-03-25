namespace FManager.Core.Dtos.DayBooks
{
    using FManager.Core.Dtos.Shared;
    using System;

    public class DayBookDto : BaseEntityWithUserDto
    {
        public Guid DayBookId { get; set; }
        public string Name { get; set; }
    }
}

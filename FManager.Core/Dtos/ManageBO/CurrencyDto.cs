namespace FManager.Core.Dtos.ManageBO
{
    using FManager.Core.Dtos.Shared;
    using System;

    public class CurrencyDto : BaseEntityWithUserDto
    {
        public Guid CurrencyId { get; set; }
        public string Name { get; set; }
    }
}

namespace FManager.Core.Dtos.ManageBO
{
    using FManager.Core.Dtos.Shared;
    using System;

    public class ParityDto : BaseEntityDto
    {
        public Guid ParityId { get; set; }
        public string Name { get; set; }
    }
}

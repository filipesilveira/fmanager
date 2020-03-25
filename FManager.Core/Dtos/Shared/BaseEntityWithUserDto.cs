namespace FManager.Core.Dtos.Shared
{
    using System;

    public class BaseEntityWithUserDto : BaseEntityDto
    {
        public Guid UserId { get; set; }
    }
}

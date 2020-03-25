namespace FManager.Core.Dtos.Shared
{
    using System;

    public class BaseEntityDto
    {
        public bool Deleted { get; set; }
        public DateTime Change { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}

using FManager.Data.Entities.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace FManager.Data.Entities.ManageBO
{
    public class Parity : BaseEntity
    {
        [Key]
        public Guid ParityId { get; set; }
        public string Name { get; set; }
    }
}
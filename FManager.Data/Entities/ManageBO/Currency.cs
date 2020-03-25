using FManager.Data.Entities.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace FManager.Data.Entities.ManageBO
{
    public class Currency : BaseEntity
    {
        [Key]
        public Guid CurrencyId { get; set; }
        public string Name { get; set; }
    }
}
using FManager.Data.Entities.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FManager.Data.Entities.ManageBO
{
    public class Account : BaseEntityWithUser
    {
        [Key]
        public Guid AccountId { get; set; }
        public string Broker { get; set; }
        public decimal InitialInvestment { get; set; }

        [ForeignKey("Currency")]
        public Guid CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }

        public virtual ICollection<Section> Sections { get; set; } = new HashSet<Section>();
    }
}
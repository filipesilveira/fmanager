using FManager.Data.Entities.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FManager.Data.Entities.ManageBO
{
    public class Section : BaseEntity
    {
        [Key]
        public Guid SectionId { get; set; }
        public string Summary { get; set; }

        [ForeignKey("Account")]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }

        public virtual ICollection<Entry> Entries { get; set; } = new HashSet<Entry>();
    }
}
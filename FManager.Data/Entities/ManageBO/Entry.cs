using FManager.Data.Entities.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FManager.Data.Entities.ManageBO
{
    public class Entry : BaseEntity
    {
        [Key]
        public Guid EntryId { get; set; }
        public DateTime DateAndTime { get; set; }
        public decimal Value { get; set; }
        public decimal Payout { get; set; }
        public bool Win { get; set; }
        public string Summary { get; set; }

        [ForeignKey("Parity")]
        public Guid ParityId { get; set; }
        public virtual Parity Parity { get; set; }

        [ForeignKey("Section")]
        public Guid SectionId { get; set; }
        public virtual Section Section { get; set; }
    }
}
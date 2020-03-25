using FManager.Data.Entities.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FManager.Data.Entities.DayBooks
{
    public class DayBookItem : BaseEntityWithUser
    {
        [Key]
        public Guid DayBookItemId { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }

        [ForeignKey("DayBook")]
        public Guid DayBookId { get; set; }
        public virtual DayBook DayBook { get; set; }
    }
}
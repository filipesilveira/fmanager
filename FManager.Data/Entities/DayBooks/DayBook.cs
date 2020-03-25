using FManager.Data.Entities.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace FManager.Data.Entities.DayBooks
{
    public class DayBook : BaseEntityWithUser
    {
        [Key]
        public Guid DayBookId { get; set; }
        public string Name { get; set; }
    }
}
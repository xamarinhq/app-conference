using System;
using System.ComponentModel.DataAnnotations;

namespace DataManager.Models
{
    public class Notification : BaseEntity
    {
        [Key]
        public string Id { get; set; }

        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}


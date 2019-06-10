using System;
using System.ComponentModel.DataAnnotations;

namespace DataManager.Models
{
    public class MiniHack : BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Subtitle { get; set; }
        [UIHint("MultilineText")]
        public string Description { get; set; }
        [UIHint("Url")]
        public string GitHubUrl {get;set;}
        [UIHint("UiImage")]
        public string BadgeUrl { get; set; }
        public string UnlockCode { get; set; }
        [Display(Name = "Score", Description = "The number of points a participant receives for completing this Mini-Hack")]
        public int Score { get; set; }
        [Display(Name = "Category", Description = "A category name to group this Mini-Hack")]
        public string Category { get; set; }
    }
}


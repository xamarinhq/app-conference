using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.Models
{
    public class SponsorLevel : BaseEntity
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// Such as Platinum
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the rank.
        /// 0 means show this sponsor level at top
        /// </summary>
        /// <value>The rank.</value>
        [Display(Name = "Rank / order", Description = "The rank or order for this sponsor level. This determines the order in which the different levels are shown.")]
        public int Rank {get;set;}
    }
}
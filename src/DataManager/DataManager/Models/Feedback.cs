using System.ComponentModel.DataAnnotations;

namespace DataManager.Models
{
    /// <summary>
    /// Per user feedback
    /// </summary>
    public class Feedback : BaseEntity
    {
        [Key]
        public string Id { get; set; }

        public string UserId { get; set; }
        public string SessionId { get; set; }
        public int SessionRating { get; set; }
    }
}
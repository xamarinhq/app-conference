using System.ComponentModel.DataAnnotations;

namespace DataManager.Models
{
    /// <summary>
    /// This is per user
    /// </summary>
    public class Favorite :BaseEntity
    {
        [Key]
        public string Id { get; set; }

        public string UserId { get; set; }
        public string SessionId { get; set; }
    }
}
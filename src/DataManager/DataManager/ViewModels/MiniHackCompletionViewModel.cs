using System;
using XamarinEvolve.Utils.Base36;

namespace DataManager.ViewModels
{
    public class MiniHackCompletionViewModel
    {
        public string UserId { get; set; }
        public DateTimeOffset CompletedAt { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Score { get; set; }

        public string UserIdBase36 => new Base36(UserId.Trim().GetHashCode()).ToString();
    }
}
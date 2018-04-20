// Helpers/Settings.cs
using Conference.DataStore.Abstractions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Conference.DataStore.Mock
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        public static bool IsFavorite(string id) =>
            Preferences.Get("fav_"+id, false);

        public static void SetFavorite(string id, bool favorite) =>
            Preferences.Set("fav_"+id, favorite);

        public static async Task ClearFavorites()
        {
            var sessions = await DependencyService.Get<ISessionStore>().GetItemsAsync();
            foreach (var session in sessions)
                Preferences.Set("fav_" + session.Id, string.Empty);
        }

        public static bool LeftFeedback(string id) =>
            Preferences.Get("feed_"+id, false);

        public static void LeaveFeedback(string id, bool leave) =>
            Preferences.Set("feed_"+id, leave);

        public static async Task ClearFeedback()
        {
            var sessions = await DependencyService.Get<ISessionStore>().GetItemsAsync();
            foreach (var session in sessions)
                Preferences.Set("feed_" + session.Id, string.Empty);
        }

    }
}
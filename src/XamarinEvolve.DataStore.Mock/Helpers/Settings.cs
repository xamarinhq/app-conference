// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinEvolve.DataStore.Abstractions;

namespace XamarinEvolve.DataStore.Mock
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }


        public static bool IsFavorite(string id) =>
            AppSettings.GetValueOrDefault<bool>("fav_"+id, false);

        public static void SetFavorite(string id, bool favorite) =>
            AppSettings.AddOrUpdateValue("fav_"+id, favorite);

        public static async Task ClearFavorites()
        {
            var sessions = await DependencyService.Get<ISessionStore>().GetItemsAsync();
            foreach (var session in sessions)
                AppSettings.Remove("fav_" + session.Id);
        }

        public static bool LeftFeedback(string id) =>
        AppSettings.GetValueOrDefault<bool>("feed_"+id, false);

        public static void LeaveFeedback(string id, bool leave) =>
        AppSettings.AddOrUpdateValue("feed_"+id, leave);

        public static async Task ClearFeedback()
        {
            var sessions = await DependencyService.Get<ISessionStore>().GetItemsAsync();
            foreach (var session in sessions)
                AppSettings.Remove("feed_" + session.Id);
        }

    }
}
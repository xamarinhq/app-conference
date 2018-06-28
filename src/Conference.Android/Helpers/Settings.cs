
// Helpers/Settings.cs This file was automatically added when you installed the Settings Plugin. If you are not using a PCL then comment this file back in to use it.
using Xamarin.Essentials;

namespace Conference.Droid.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
        const string NotificationIdKey = "notification_id";
        static readonly int NotificationIdDefault = 0;

        public static int NotificationId
        {
            get { return Preferences.Get(NotificationIdKey, NotificationIdDefault); }
            set { Preferences.Set(NotificationIdKey, value); }
        }

        public static int GetUniqueNotificationId()
        {
            return NotificationId++;
        }

  }
}
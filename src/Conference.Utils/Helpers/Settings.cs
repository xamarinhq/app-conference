// Helpers/Settings.cs
using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;

namespace Conference.Clients.Portable
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public class Settings : INotifyPropertyChanged
    {
        static Settings settings;

        /// <summary>
        /// Gets or sets the current settings. This should always be used
        /// </summary>
        /// <value>The current.</value>
        public static Settings Current
        {
            get { return settings ?? (settings = new Settings()); }
        }


        const string GcmTokenKey = "gcm_token";
        readonly string GcmTokenDefault = string.Empty;
        public string GcmToken
        {
            get { return Preferences.Get(GcmTokenKey, GcmTokenDefault); }
            set { Preferences.Set(GcmTokenKey, value); OnPropertyChanged(); }
        }

        const string WiFiSSIDKey = "ssid_key";
        readonly string WiFiSSIDDefault = "Xamarin_Conference";
        public string WiFiSSID 
        {
            get { return Preferences.Get(WiFiSSIDKey, WiFiSSIDDefault); }
            set { Preferences.Set(WiFiSSIDKey, value); OnPropertyChanged(); }
        }

        const string WiFiPassKey = "wifi_pass_key";
        readonly string WiFiPassDefault = "";
        public string WiFiPass 
        {
            get { return Preferences.Get(WiFiPassKey, WiFiPassDefault); }
            set { Preferences.Set(WiFiPassKey, value); OnPropertyChanged(); }
        }

        public void SaveReminderId(string id, string calId)
        {
            Preferences.Set(GetReminderId(id), calId);
        }

        string GetReminderId(string id)
        {
            return "reminder_" + id;
        }

        public string GetEventId(string id)
        {
            return Preferences.Get(GetReminderId(id), string.Empty);
        }

        public void RemoveReminderId(string id)
        {
            Preferences.Set(GetReminderId(id), string.Empty);
        }

        public bool IsHackFinished(string id)
        {
            return Preferences.Get("minihack_" + id, false);
        }

        public void FinishHack(string id)
        {
            Preferences.Set("minihack_" + id, true);
        }

        const string LastFavoriteTimeKey = "last_favorite_time";

        public DateTime LastFavoriteTime
        {
            get { return new DateTime(Preferences.Get(LastFavoriteTimeKey, DateTime.UtcNow.Ticks)); }
            set { Preferences.Set(LastFavoriteTimeKey, value.Ticks); }
        }


        const string HasSetReminderKey = "set_a_reminder";
        static readonly bool HasSetReminderDefault = false;

        public bool HasSetReminder
        {
            get { return Preferences.Get(HasSetReminderKey, HasSetReminderDefault); }
            set
            {
                Preferences.Set(HasSetReminderKey, value);
            }
        }

        const string ConferenceCalendarIdKey = "conference_calendar";
        static readonly string ConferenceCalendarIdDefault = string.Empty;
        public string ConferenceCalendarId
        {
            get { return Preferences.Get(ConferenceCalendarIdKey, ConferenceCalendarIdDefault); }
            set { Preferences.Set(ConferenceCalendarIdKey, value); }
        }
          

        const string PushNotificationsEnabledKey = "push_enabled";
        static readonly bool PushNotificationsEnabledDefault = false;

        public bool PushNotificationsEnabled
        {
            get { return Preferences.Get(PushNotificationsEnabledKey, PushNotificationsEnabledDefault); }
            set { Preferences.Set(PushNotificationsEnabledKey, value); OnPropertyChanged(); }
        }

        const string FirstRunKey = "first_run";
        static readonly bool FirstRunDefault = true;

        /// <summary>
        /// Gets or sets a value indicating whether the user wants to see favorites only.
        /// </summary>
        /// <value><c>true</c> if favorites only; otherwise, <c>false</c>.</value>
        public bool FirstRun
        {
            get { return Preferences.Get(FirstRunKey, FirstRunDefault); }
            set { Preferences.Set(FirstRunKey, value); OnPropertyChanged(); }
        }

        const string GooglePlayCheckedKey = "play_checked";
        static readonly bool GooglePlayCheckedDefault = false;

        public bool GooglePlayChecked
        {
            get { return Preferences.Get(GooglePlayCheckedKey, GooglePlayCheckedDefault); }
            set
            {
                Preferences.Set(GooglePlayCheckedKey, value);
            }
        }

        const string AttemptedPushKey = "attempted_push";
        static readonly bool AttemptedPushDefault = false;

        public bool AttemptedPush
        {
            get { return Preferences.Get(AttemptedPushKey, AttemptedPushDefault); }
            set { Preferences.Set(AttemptedPushKey, value); }
        }

       
        const string PushRegisteredKey = "push_registered";
        static readonly bool PushRegisteredDefault = false;

        public bool PushRegistered
        {
            get { return Preferences.Get(PushRegisteredKey, PushRegisteredDefault); }
            set { Preferences.Set(PushRegisteredKey, value); }
        }

        const string FavoriteModeKey = "favorites_only";
        static readonly bool FavoriteModeDefault = false;

        /// <summary>
        /// Gets or sets a value indicating whether the user wants to see favorites only.
        /// </summary>
        /// <value><c>true</c> if favorites only; otherwise, <c>false</c>.</value>
        public bool FavoritesOnly
        {
            get { return Preferences.Get(FavoriteModeKey, FavoriteModeDefault); }
            set { Preferences.Set(FavoriteModeKey, value); OnPropertyChanged(); }
        }

        const string ShowAllCategoriesKey = "all_categories";
        static readonly bool ShowAllCategoriesDefault = true;

        /// <summary>
        /// Gets or sets a value indicating whether the user wants to show all categories.
        /// </summary>
        /// <value><c>true</c> if show all categories; otherwise, <c>false</c>.</value>
        public bool ShowAllCategories
        {
            get { return Preferences.Get(ShowAllCategoriesKey, ShowAllCategoriesDefault); }
            set { Preferences.Set(ShowAllCategoriesKey, value); OnPropertyChanged(); }
        }

        const string ShowPastSessionsKey = "show_past_sessions";
        static readonly bool ShowPastSessionsDefault = false;
        public static readonly DateTime EndOfConference = new DateTime(2016, 4, 29, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Gets or sets a value indicating whether the user wants show past sessions.
        /// </summary>
        /// <value><c>true</c> if show past sessions; otherwise, <c>false</c>.</value>
        public bool ShowPastSessions
        {
            get 
            {
                //if end of conference
                if (DateTime.UtcNow > EndOfConference)
                    return true;
                
                return Preferences.Get(ShowPastSessionsKey, ShowPastSessionsDefault); 
            }
            set { Preferences.Set(ShowPastSessionsKey, value); OnPropertyChanged(); }
        }

        const string FilteredCategoriesKey = "filtered_categories";
        static readonly string FilteredCategoriesDefault = string.Empty;


        public string FilteredCategories
        {
            get { return Preferences.Get(FilteredCategoriesKey, FilteredCategoriesDefault); }
            set { Preferences.Set(FilteredCategoriesKey, value); OnPropertyChanged(); }
        }


        const string EmailKey = "email_key";
        readonly string EmailDefault = string.Empty;
        public string Email 
        {
            get { return Preferences.Get(EmailKey, EmailDefault); }
            set
            {
                Preferences.Set(EmailKey, value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(UserAvatar));
            }
        }

        const string DatabaseIdKey = "azure_database";
        static readonly int DatabaseIdDefault = 0;

        public static int DatabaseId
        {
            get { return Preferences.Get(DatabaseIdKey, DatabaseIdDefault); }
            set { Preferences.Set(DatabaseIdKey, value); }
        }

        public static int UpdateDatabaseId()
        {
            return DatabaseId++;
        }

        const string FirstNameKey = "firstname_key";
        readonly string FirstNameDefault =  string.Empty;
        public string FirstName 
        {
            get { return Preferences.Get(FirstNameKey, FirstNameDefault); }
            set
            {
                Preferences.Set(FirstNameKey, value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(UserDisplayName));
            }
        }

        const string LastNameKey = "lastname_key";
        readonly string LastNameDefault =  string.Empty;
        public string LastName 
        {
            get { return Preferences.Get(LastNameKey, LastNameDefault); }
            set
            {
                Preferences.Set(LastNameKey, value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(UserDisplayName));
            }
        }


        const string NeedsSyncKey = "needs_sync";
        const bool NeedsSyncDefault = true;
        public bool NeedsSync
        {
            get { return Preferences.Get(NeedsSyncKey, NeedsSyncDefault) || LastSync < DateTime.Now.AddDays(-1); }
            set { Preferences.Set(NeedsSyncKey, value); }

        }

        const string LoginAttemptsKey = "login_attempts";
        const int LoginAttemptsDefault = 0;
        public int LoginAttempts
        {
            get { return Preferences.Get(LoginAttemptsKey, LoginAttemptsDefault); }
            set { Preferences.Set(LoginAttemptsKey, value); }
        }

        const string HasSyncedDataKey = "has_synced";
        const bool HasSyncedDataDefault = false;
        public bool HasSyncedData
        {
            get { return Preferences.Get(HasSyncedDataKey, HasSyncedDataDefault); }
            set { Preferences.Set(HasSyncedDataKey, value); }

        }

        const string LastSyncKey = "last_sync";
        static readonly DateTime LastSyncDefault = DateTime.Now.AddDays(-30);
        public DateTime LastSync
        {
            get { return new DateTime(Preferences.Get(LastSyncKey, LastSyncDefault.Ticks)); }
            set { Preferences.Set(LastSyncKey, value.Ticks); OnPropertyChanged(); }
        } 

        bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
            set 
            { 
                if (isConnected == value)
                    return;
                isConnected = value;
                OnPropertyChanged();
            }
        }

        #region Helpers


        public string UserDisplayName => IsLoggedIn ? $"{FirstName} {LastName}" : "Sign In";

        public string UserAvatar => IsLoggedIn ? Gravatar.GetURL(Email) : "profile_generic.png";

        public bool IsLoggedIn => !string.IsNullOrWhiteSpace(Email);

        public bool HasFilters => (ShowPastSessions || FavoritesOnly || (!string.IsNullOrWhiteSpace(FilteredCategories) && !ShowAllCategories));

        #endregion

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
    }
}
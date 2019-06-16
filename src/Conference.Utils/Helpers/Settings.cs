// Helpers/Settings.cs
using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

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
        public static Settings Current => settings ?? (settings = new Settings());

        IPlatformSpecificSettings platformSettings;

        public Settings()
        {
            platformSettings = DependencyService.Get<IPlatformSpecificSettings>();
        }


        const string gcmTokenKey = "gcm_token";
        readonly string gcmTokenDefault = string.Empty;
        public string GcmToken
        {
            get => Preferences.Get(gcmTokenKey, gcmTokenDefault);
            set { Preferences.Set(gcmTokenKey, value); OnPropertyChanged(); }
        }

        const string wiFiSSIDKey = "ssid_key";
        readonly string wiFiSSIDDefault = "Xamarin_Conference";
        public string WiFiSSID
        {
            get => Preferences.Get(wiFiSSIDKey, wiFiSSIDDefault);
            set { Preferences.Set(wiFiSSIDKey, value); OnPropertyChanged(); }
        }

        const string wiFiPassKey = "wifi_pass_key";
        readonly string wiFiPassDefault = "";
        public string WiFiPass
        {
            get => Preferences.Get(wiFiPassKey, wiFiPassDefault);
            set { Preferences.Set(wiFiPassKey, value); OnPropertyChanged(); }
        }

        public void SaveReminderId(string id, string calId) => Preferences.Set(GetReminderId(id), calId);

        string GetReminderId(string id) => "reminder_" + id;

        public string GetEventId(string id) => Preferences.Get(GetReminderId(id), string.Empty);

        public void RemoveReminderId(string id) => Preferences.Set(GetReminderId(id), string.Empty);

        public bool IsHackFinished(string id) => Preferences.Get("minihack_" + id, false);

        public void FinishHack(string id) => Preferences.Set("minihack_" + id, true);

        const string lastFavoriteTimeKey = "last_favorite_time";

        public DateTime LastFavoriteTime
        {
            get => new DateTime(Preferences.Get(lastFavoriteTimeKey, DateTime.UtcNow.Ticks));
            set => Preferences.Set(lastFavoriteTimeKey, value.Ticks);
        }


        const string hasSetReminderKey = "set_a_reminder";
        static readonly bool hasSetReminderDefault = false;

        public bool HasSetReminder
        {
            get => Preferences.Get(hasSetReminderKey, hasSetReminderDefault);
            set => Preferences.Set(hasSetReminderKey, value);
        }

        const string conferenceCalendarIdKey = "conference_calendar";
        static readonly string conferenceCalendarIdDefault = string.Empty;
        public string ConferenceCalendarId
        {
            get => Preferences.Get(conferenceCalendarIdKey, conferenceCalendarIdDefault);
            set => Preferences.Set(conferenceCalendarIdKey, value);
        }


        const string pushNotificationsEnabledKey = "push_enabled";
        static readonly bool pushNotificationsEnabledDefault = false;

        public bool PushNotificationsEnabled
        {
            get => Preferences.Get(pushNotificationsEnabledKey, pushNotificationsEnabledDefault);
            set { Preferences.Set(pushNotificationsEnabledKey, value); OnPropertyChanged(); }
        }

        const string firstRunKey = "first_run";
        static readonly bool firstRunDefault = true;

        /// <summary>
        /// Gets or sets a value indicating whether the user wants to see favorites only.
        /// </summary>
        /// <value><c>true</c> if favorites only; otherwise, <c>false</c>.</value>
        public bool FirstRun
        {
            get => Preferences.Get(firstRunKey, firstRunDefault);
            set { Preferences.Set(firstRunKey, value); OnPropertyChanged(); }
        }

        const string googlePlayCheckedKey = "play_checked";
        static readonly bool googlePlayCheckedDefault = false;

        public bool GooglePlayChecked
        {
            get => Preferences.Get(googlePlayCheckedKey, googlePlayCheckedDefault);
            set => Preferences.Set(googlePlayCheckedKey, value);
        }

        const string attemptedPushKey = "attempted_push";
        static readonly bool attemptedPushDefault = false;

        public bool AttemptedPush
        {
            get => Preferences.Get(attemptedPushKey, attemptedPushDefault);
            set => Preferences.Set(attemptedPushKey, value);
        }


        const string pushRegisteredKey = "push_registered";
        static readonly bool pushRegisteredDefault = false;

        public bool PushRegistered
        {
            get => Preferences.Get(pushRegisteredKey, pushRegisteredDefault);
            set => Preferences.Set(pushRegisteredKey, value);
        }

        const string favoriteModeKey = "favorites_only";
        static readonly bool favoriteModeDefault = false;

        /// <summary>
        /// Gets or sets a value indicating whether the user wants to see favorites only.
        /// </summary>
        /// <value><c>true</c> if favorites only; otherwise, <c>false</c>.</value>
        public bool FavoritesOnly
        {
            get => Preferences.Get(favoriteModeKey, favoriteModeDefault);
            set { Preferences.Set(favoriteModeKey, value); OnPropertyChanged(); }
        }

        const string showAllCategoriesKey = "all_categories";
        static readonly bool showAllCategoriesDefault = true;

        /// <summary>
        /// Gets or sets a value indicating whether the user wants to show all categories.
        /// </summary>
        /// <value><c>true</c> if show all categories; otherwise, <c>false</c>.</value>
        public bool ShowAllCategories
        {
            get => Preferences.Get(showAllCategoriesKey, showAllCategoriesDefault);
            set { Preferences.Set(showAllCategoriesKey, value); OnPropertyChanged(); }
        }

        const string showPastSessionsKey = "show_past_sessions";
        static readonly bool showPastSessionsDefault = false;
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
                
                return Preferences.Get(showPastSessionsKey, showPastSessionsDefault); 
            }
            set { Preferences.Set(showPastSessionsKey, value); OnPropertyChanged(); }
        }

        const string filteredCategoriesKey = "filtered_categories";
        static readonly string filteredCategoriesDefault = string.Empty;


        public string FilteredCategories
        {
            get => Preferences.Get(filteredCategoriesKey, filteredCategoriesDefault);
            set { Preferences.Set(filteredCategoriesKey, value); OnPropertyChanged(); }
        }


        const string emailKey = "email_key";
        readonly string emailDefault = string.Empty;
        public string Email
        {
            get => Preferences.Get(emailKey, emailDefault);
            set
            {
                Preferences.Set(emailKey, value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(UserAvatar));
            }
        }

        const string userIdentifierKey = "useridentifier_key";
        readonly string userIdentifierDefault = string.Empty;
        public string UserIdentifier
        {
            get
            {
                var id = Preferences.Get(userIdentifierKey, userIdentifierDefault);

                if (platformSettings != null && platformSettings.UserIdentifier != id)
                {
                    platformSettings.UserIdentifier = id;
                }
                return id;
            }
            set
            {
                Preferences.Set(userIdentifierKey, value);
                if (platformSettings != null)
                {
                    platformSettings.UserIdentifier = value;
                }
                OnPropertyChanged();
            }
        }

        const string databaseIdKey = "azure_database";
        static readonly int databaseIdDefault = 0;

        public static int DatabaseId
        {
            get => Preferences.Get(databaseIdKey, databaseIdDefault);
            set => Preferences.Set(databaseIdKey, value);
        }

        public static int UpdateDatabaseId() => DatabaseId++;

        const string firstNameKey = "firstname_key";
        readonly string firstNameDefault =  string.Empty;
        public string FirstName
        {
            get => Preferences.Get(firstNameKey, firstNameDefault);
            set
            {
                Preferences.Set(firstNameKey, value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(UserDisplayName));
            }
        }

        const string lastNameKey = "lastname_key";
        readonly string lastNameDefault =  string.Empty;
        public string LastName
        {
            get => Preferences.Get(lastNameKey, lastNameDefault);
            set
            {
                Preferences.Set(lastNameKey, value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(UserDisplayName));
            }
        }


        const string needsSyncKey = "needs_sync";
        const bool needsSyncDefault = true;
        public bool NeedsSync
        {
            get => Preferences.Get(needsSyncKey, needsSyncDefault) || LastSync < DateTime.Now.AddDays(-1);
            set => Preferences.Set(needsSyncKey, value);

        }

        const string loginAttemptsKey = "login_attempts";
        const int loginAttemptsDefault = 0;
        public int LoginAttempts
        {
            get => Preferences.Get(loginAttemptsKey, loginAttemptsDefault);
            set => Preferences.Set(loginAttemptsKey, value);
        }

        const string hasSyncedDataKey = "has_synced";
        const bool hasSyncedDataDefault = false;
        public bool HasSyncedData
        {
            get => Preferences.Get(hasSyncedDataKey, hasSyncedDataDefault);
            set => Preferences.Set(hasSyncedDataKey, value);

        }

        const string lastSyncKey = "last_sync";
        static readonly DateTime lastSyncDefault = DateTime.Now.AddDays(-30);
        public DateTime LastSync
        {
            get => Preferences.Get(lastSyncKey, lastSyncDefault);
            set { Preferences.Set(lastSyncKey, value); OnPropertyChanged(); }
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
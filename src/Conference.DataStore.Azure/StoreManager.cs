using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Conference.DataObjects;
using Conference.DataStore.Abstractions;
using System.Collections.Generic;
using Conference.Clients.Portable;

namespace Conference.DataStore.Azure
{
    public class StoreManager : IStoreManager
    {
        
        public static MobileServiceClient MobileService { get; set; }

        /// <summary>
        /// Syncs all tables.
        /// </summary>
        /// <returns>The all async.</returns>
        /// <param name="syncUserSpecific">If set to <c>true</c> sync user specific.</param>
        public async Task<bool> SyncAllAsync(bool syncUserSpecific)
        {
            if(!IsInitialized)
                await InitializeAsync();
            
            var taskList = new List<Task<bool>>();
            taskList.Add(CategoryStore.SyncAsync());
            taskList.Add(MiniHacksStore.SyncAsync());
            taskList.Add(NotificationStore.SyncAsync());
            taskList.Add(SpeakerStore.SyncAsync());
            taskList.Add(SessionStore.SyncAsync());
            taskList.Add(SponsorStore.SyncAsync());
            taskList.Add(EventStore.SyncAsync());


            if (syncUserSpecific)
            {
                taskList.Add(FeedbackStore.SyncAsync());
                taskList.Add(FavoriteStore.SyncAsync());
            }

            var successes = await Task.WhenAll(taskList).ConfigureAwait(false);
            return successes.Any(x => !x);//if any were a failure.
        }

        /// <summary>
        /// Drops all tables from the database and updated DB Id
        /// </summary>
        /// <returns>The everything async.</returns>
        public Task DropEverythingAsync()
        {
            Settings.UpdateDatabaseId();
            CategoryStore.DropTable();
            EventStore.DropTable();
            MiniHacksStore.DropTable();
            NotificationStore.DropTable();
            SessionStore.DropTable();
            SpeakerStore.DropTable();
            SponsorStore.DropTable();
            FeedbackStore.DropTable();
            FavoriteStore.DropTable();
            IsInitialized = false;
            return Task.FromResult(true);
        }




        public bool IsInitialized { get; private set; }
        #region IStoreManager implementation
        object locker = new object ();
        public async Task InitializeAsync()
        {
            MobileServiceSQLiteStore store;
            lock(locker) 
            {

                if (IsInitialized)
                    return;
                
                IsInitialized = true;
                var dbId = Settings.DatabaseId;
                var path = $"syncstore{dbId}.db";
                MobileService = new MobileServiceClient ("https://xamarinevolveappdemo.azurewebsites.net");
                store = new MobileServiceSQLiteStore (path);
                store.DefineTable<Category> ();
                store.DefineTable<Favorite> ();
                store.DefineTable<Notification> ();
                store.DefineTable<FeaturedEvent> ();
                store.DefineTable<Feedback> ();
                store.DefineTable<Room> ();
                store.DefineTable<Session> ();
                store.DefineTable<Speaker> ();
                store.DefineTable<Sponsor> ();
                store.DefineTable<SponsorLevel> ();
                store.DefineTable<StoreSettings> ();
                store.DefineTable<MiniHack> ();
            }

            await MobileService.SyncContext.InitializeAsync (store, new MobileServiceSyncHandler ()).ConfigureAwait (false);

            await LoadCachedTokenAsync ().ConfigureAwait (false);

        }

        IMiniHacksStore miniHacksStore;
        public IMiniHacksStore MiniHacksStore => miniHacksStore ?? (miniHacksStore  = DependencyService.Get<IMiniHacksStore>());
       
        INotificationStore notificationStore;
        public INotificationStore NotificationStore => notificationStore ?? (notificationStore  = DependencyService.Get<INotificationStore>());


        ICategoryStore categoryStore;
        public ICategoryStore CategoryStore => categoryStore ?? (categoryStore  = DependencyService.Get<ICategoryStore>());

        IFavoriteStore favoriteStore;
        public IFavoriteStore FavoriteStore => favoriteStore ?? (favoriteStore  = DependencyService.Get<IFavoriteStore>());

        IFeedbackStore feedbackStore;
        public IFeedbackStore FeedbackStore => feedbackStore ?? (feedbackStore  = DependencyService.Get<IFeedbackStore>());

        ISessionStore sessionStore;
        public ISessionStore SessionStore => sessionStore ?? (sessionStore  = DependencyService.Get<ISessionStore>());
     

        ISpeakerStore speakerStore;
        public ISpeakerStore SpeakerStore => speakerStore ?? (speakerStore  = DependencyService.Get<ISpeakerStore>());

        IEventStore eventStore;
        public IEventStore EventStore => eventStore ?? (eventStore = DependencyService.Get<IEventStore>());

        ISponsorStore sponsorStore;
        public ISponsorStore SponsorStore => sponsorStore ?? (sponsorStore  = DependencyService.Get<ISponsorStore>());


        #endregion

        public async Task<MobileServiceUser> LoginAsync(string username, string password)
        {
            if (!IsInitialized)
            {
                await InitializeAsync();
            }

            var credentials = new JObject();
            credentials["email"] = username;
            credentials["password"] = password;

            var user = await MobileService.LoginAsync("Xamarin", credentials);

            await CacheToken(user);

            return user;
        }

        public async Task LogoutAsync()
        {
            if (!IsInitialized)
            {
                await InitializeAsync();
            }

            await MobileService.LogoutAsync();

            var settings = await ReadSettingsAsync();

            if (settings != null)
            {
                settings.AuthToken = string.Empty;
                settings.UserId = string.Empty;

                await SaveSettingsAsync(settings);
            }
        }

        async Task SaveSettingsAsync(StoreSettings settings) =>
            await MobileService.SyncContext.Store.UpsertAsync(nameof(StoreSettings), new[] { JObject.FromObject(settings) }, true);

        async Task<StoreSettings> ReadSettingsAsync() =>
            (await MobileService.SyncContext.Store.LookupAsync(nameof(StoreSettings), StoreSettings.StoreSettingsId))?.ToObject<StoreSettings>();
        

        async Task CacheToken(MobileServiceUser user)
        {
            var settings = new StoreSettings
            {
                UserId = user.UserId,
                AuthToken = user.MobileServiceAuthenticationToken
            };

            await SaveSettingsAsync(settings);
            
        }

        async Task LoadCachedTokenAsync()
        {
            var settings = await ReadSettingsAsync();

            if (settings != null)
            {
                try
                {
                    if (!string.IsNullOrEmpty(settings.AuthToken) && JwtUtility.GetTokenExpiration(settings.AuthToken) > DateTime.UtcNow)
                    {
                        MobileService.CurrentUser = new MobileServiceUser(settings.UserId);
                        MobileService.CurrentUser.MobileServiceAuthenticationToken = settings.AuthToken;
                    }
                }
                catch (InvalidTokenException)
                {
                    settings.AuthToken = string.Empty;
                    settings.UserId = string.Empty;

                    await SaveSettingsAsync(settings);
                }
            }
        }

        public class StoreSettings
        {
            public const string StoreSettingsId = "store_settings";

            public StoreSettings()
            {
                Id = StoreSettingsId;
            }

            public string Id { get; set; }

            public string UserId { get; set; }

            public string AuthToken { get; set; }
        }
    }
}


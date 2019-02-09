using Xamarin.Forms;
using Conference.DataStore.Abstractions;

using MvvmHelpers;
using System.Threading.Tasks;
using System.Windows.Input;
using System;

using Xamarin.Essentials;

namespace Conference.Clients.Portable
{
    public class ViewModelBase : BaseViewModel
    {

        protected INavigation Navigation { get; }

        public ViewModelBase(INavigation navigation = null)
        {
            Navigation = navigation;
        }

        public static void Init (bool mock = true)
        {

#if ENABLE_TEST_CLOUD && !DEBUG
                DependencyService.Register<ISessionStore, Conference.DataStore.Mock.SessionStore>();
                DependencyService.Register<IFavoriteStore, Conference.DataStore.Mock.FavoriteStore>();
                DependencyService.Register<IFeedbackStore, Conference.DataStore.Mock.FeedbackStore>();
                DependencyService.Register<ISpeakerStore, Conference.DataStore.Mock.SpeakerStore>();
                DependencyService.Register<ISponsorStore, Conference.DataStore.Mock.SponsorStore>();
                DependencyService.Register<ICategoryStore, Conference.DataStore.Mock.CategoryStore>();
                DependencyService.Register<IEventStore, Conference.DataStore.Mock.EventStore>();
                DependencyService.Register<INotificationStore, Conference.DataStore.Mock.NotificationStore>();
                DependencyService.Register<IMiniHacksStore, Conference.DataStore.Mock.MiniHacksStore>();
                DependencyService.Register<ISSOClient, Conference.Clients.Portable.Auth.XamarinSSOClient>();
                DependencyService.Register<IStoreManager, Conference.DataStore.Mock.StoreManager>();
#else
            if (mock) 
            {
                DependencyService.Register<ISessionStore, Conference.DataStore.Mock.SessionStore> ();
                DependencyService.Register<IFavoriteStore, Conference.DataStore.Mock.FavoriteStore> ();
                DependencyService.Register<IFeedbackStore, Conference.DataStore.Mock.FeedbackStore> ();
                DependencyService.Register<ISpeakerStore, Conference.DataStore.Mock.SpeakerStore> ();
                DependencyService.Register<ISponsorStore, Conference.DataStore.Mock.SponsorStore> ();
                DependencyService.Register<ICategoryStore, Conference.DataStore.Mock.CategoryStore> ();
                DependencyService.Register<IEventStore, Conference.DataStore.Mock.EventStore> ();
                DependencyService.Register<INotificationStore, Conference.DataStore.Mock.NotificationStore> ();
                DependencyService.Register<IMiniHacksStore, Conference.DataStore.Mock.MiniHacksStore> ();
                DependencyService.Register<ISSOClient, Conference.Clients.Portable.Auth.XamarinSSOClient> ();
                DependencyService.Register<IStoreManager, Conference.DataStore.Mock.StoreManager> ();
            } 
            else 
            {
                DependencyService.Register<ISessionStore, Conference.DataStore.Azure.SessionStore> ();
                DependencyService.Register<IFavoriteStore, Conference.DataStore.Azure.FavoriteStore> ();
                DependencyService.Register<IFeedbackStore, Conference.DataStore.Azure.FeedbackStore> ();
                DependencyService.Register<ISpeakerStore, Conference.DataStore.Azure.SpeakerStore> ();
                DependencyService.Register<ISponsorStore, Conference.DataStore.Azure.SponsorStore> ();
                DependencyService.Register<ICategoryStore, Conference.DataStore.Azure.CategoryStore> ();
                DependencyService.Register<IEventStore, Conference.DataStore.Azure.EventStore> ();
                DependencyService.Register<INotificationStore, Conference.DataStore.Azure.NotificationStore> ();
                DependencyService.Register<IMiniHacksStore, Conference.DataStore.Azure.MiniHacksStore> ();
                DependencyService.Register<ISSOClient, Conference.Clients.Portable.Auth.Azure.XamarinSSOClient> ();
                DependencyService.Register<IStoreManager, Conference.DataStore.Azure.StoreManager> ();
            }


            #endif


            DependencyService.Register<FavoriteService>();
        }


        protected ILogger Logger { get; } = DependencyService.Get<ILogger>();
        protected IStoreManager StoreManager { get; }  = DependencyService.Get<IStoreManager>();
        protected IToast Toast { get; }  = DependencyService.Get<IToast>();

        protected FavoriteService FavoriteService { get; } = DependencyService.Get<FavoriteService>();


        public Settings Settings
        {
            get { return Settings.Current; }
        }

        ICommand  launchBrowserCommand;
        public ICommand LaunchBrowserCommand =>
        launchBrowserCommand ?? (launchBrowserCommand = new Command<string>(async (t) => await ExecuteLaunchBrowserAsync(t))); 

        async Task ExecuteLaunchBrowserAsync(string arg)
        {
            if(IsBusy)
                return;

            if (!arg.StartsWith ("http://", StringComparison.OrdinalIgnoreCase) && !arg.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                arg = "http://" + arg;
            
            Logger.Track(ConferenceLoggerKeys.LaunchedBrowser, "Url", arg);

            var lower = arg.ToLowerInvariant();
            if(Device.RuntimePlatform == Device.iOS && lower.Contains("twitter.com"))
            {
                try
                {
                    var id = arg.Substring(lower.LastIndexOf("/", StringComparison.Ordinal) + 1);
                    var launchTwitter = DependencyService.Get<ILaunchTwitter>();
                    if(lower.Contains("/status/"))
                    {
                        //status
                        if(launchTwitter.OpenStatus(id))
                            return;
                    }
                    else
                    {
                        //user
                        if(launchTwitter.OpenUserName(id))
                            return;
                    }
                }
                catch
                {
                }
            }

            try 
            {
                await Browser.OpenAsync(arg);
            } 
            catch 
            {
            }
        }

       

    }
}



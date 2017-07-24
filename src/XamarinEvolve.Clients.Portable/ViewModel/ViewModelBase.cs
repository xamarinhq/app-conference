using Xamarin.Forms;
using XamarinEvolve.DataStore.Abstractions;

using MvvmHelpers;
using Plugin.Share;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Share.Abstractions;
using System;

namespace XamarinEvolve.Clients.Portable
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
                DependencyService.Register<ISessionStore, XamarinEvolve.DataStore.Mock.SessionStore>();
                DependencyService.Register<IFavoriteStore, XamarinEvolve.DataStore.Mock.FavoriteStore>();
                DependencyService.Register<IFeedbackStore, XamarinEvolve.DataStore.Mock.FeedbackStore>();
                DependencyService.Register<ISpeakerStore, XamarinEvolve.DataStore.Mock.SpeakerStore>();
                DependencyService.Register<ISponsorStore, XamarinEvolve.DataStore.Mock.SponsorStore>();
                DependencyService.Register<ICategoryStore, XamarinEvolve.DataStore.Mock.CategoryStore>();
                DependencyService.Register<IEventStore, XamarinEvolve.DataStore.Mock.EventStore>();
                DependencyService.Register<INotificationStore, XamarinEvolve.DataStore.Mock.NotificationStore>();
                DependencyService.Register<IMiniHacksStore, XamarinEvolve.DataStore.Mock.MiniHacksStore>();
                DependencyService.Register<ISSOClient, XamarinEvolve.Clients.Portable.Auth.XamarinSSOClient>();
                DependencyService.Register<IStoreManager, XamarinEvolve.DataStore.Mock.StoreManager>();
#else
            if (mock) 
            {
                DependencyService.Register<ISessionStore, XamarinEvolve.DataStore.Mock.SessionStore> ();
                DependencyService.Register<IFavoriteStore, XamarinEvolve.DataStore.Mock.FavoriteStore> ();
                DependencyService.Register<IFeedbackStore, XamarinEvolve.DataStore.Mock.FeedbackStore> ();
                DependencyService.Register<ISpeakerStore, XamarinEvolve.DataStore.Mock.SpeakerStore> ();
                DependencyService.Register<ISponsorStore, XamarinEvolve.DataStore.Mock.SponsorStore> ();
                DependencyService.Register<ICategoryStore, XamarinEvolve.DataStore.Mock.CategoryStore> ();
                DependencyService.Register<IEventStore, XamarinEvolve.DataStore.Mock.EventStore> ();
                DependencyService.Register<INotificationStore, XamarinEvolve.DataStore.Mock.NotificationStore> ();
                DependencyService.Register<IMiniHacksStore, XamarinEvolve.DataStore.Mock.MiniHacksStore> ();
                DependencyService.Register<ISSOClient, XamarinEvolve.Clients.Portable.Auth.XamarinSSOClient> ();
                DependencyService.Register<IStoreManager, XamarinEvolve.DataStore.Mock.StoreManager> ();
            } 
            else 
            {
                DependencyService.Register<ISessionStore, XamarinEvolve.DataStore.Azure.SessionStore> ();
                DependencyService.Register<IFavoriteStore, XamarinEvolve.DataStore.Azure.FavoriteStore> ();
                DependencyService.Register<IFeedbackStore, XamarinEvolve.DataStore.Azure.FeedbackStore> ();
                DependencyService.Register<ISpeakerStore, XamarinEvolve.DataStore.Azure.SpeakerStore> ();
                DependencyService.Register<ISponsorStore, XamarinEvolve.DataStore.Azure.SponsorStore> ();
                DependencyService.Register<ICategoryStore, XamarinEvolve.DataStore.Azure.CategoryStore> ();
                DependencyService.Register<IEventStore, XamarinEvolve.DataStore.Azure.EventStore> ();
                DependencyService.Register<INotificationStore, XamarinEvolve.DataStore.Azure.NotificationStore> ();
                DependencyService.Register<IMiniHacksStore, XamarinEvolve.DataStore.Azure.MiniHacksStore> ();
                DependencyService.Register<ISSOClient, XamarinEvolve.Clients.Portable.Auth.Azure.XamarinSSOClient> ();
                DependencyService.Register<IStoreManager, XamarinEvolve.DataStore.Azure.StoreManager> ();
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
            
            Logger.Track(EvolveLoggerKeys.LaunchedBrowser, "Url", arg);

            var lower = arg.ToLowerInvariant();
            if(Device.OS == TargetPlatform.iOS && lower.Contains("twitter.com"))
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
                await CrossShare.Current.OpenBrowser (arg, new BrowserOptions {
                    ChromeShowTitle = true,
                    ChromeToolbarColor = new ShareColor {
                        A = 255,
                        R = 118,
                        G = 53,
                        B = 235
                    },
                    UseSafariReaderMode = true,
                    UseSafariWebViewController = true
                });
            } 
            catch 
            {
            }
        }

       

    }
}



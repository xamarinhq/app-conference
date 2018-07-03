using System;
using System.Collections.Generic;
using FormsToolkit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Conference.Clients.Portable;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Distribute;
using Microsoft.AppCenter.Crashes;

[assembly:Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]

namespace Conference.Clients.UI
{
    public partial class App : Application
    {
        public static App current;
        public App()
        {
            current = this;
            InitializeComponent();
            ViewModelBase.Init();

#if !DEBUG
            Microsoft.AppCenter.AppCenter.Start("uwp=c9066a4a-7a4d-4b2c-9146-66736339398b;" 
                + "android=0da69ace-2d11-498f-8e7f-e706b7c31a05"
                + "ios=508b163d-4fd0-4185-8302-e733e6504d3f", typeof(Analytics), typeof(Distribute), typeof(Crashes));
#endif

            // The root page of your application
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    MainPage = new RootPageAndroid();
                    break;
                case Device.iOS:
                    MainPage = new ConferenceNavigationPage(new RootPageiOS());
                    break;
                case Device.UWP:
                    MainPage = new RootPageWindows();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        static ILogger logger;
        public static ILogger Logger => logger ?? (logger = DependencyService.Get<ILogger>());


        protected override void OnStart()
        {
           OnResume();
        }

        public void SecondOnResume()
        {
            OnResume();
        }

        bool registered;
        bool firstRun = true;
        protected override void OnResume()
        {
            if (registered)
                return;
            registered = true;
            // Handle when your app resumes
            Settings.Current.IsConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;
            Connectivity.ConnectivityChanged += ConnectivityChanged;

            // Handle when your app starts
            MessagingService.Current.Subscribe<MessagingServiceAlert>(MessageKeys.Message, async (m, info) =>
                {
                    var task = Application.Current?.MainPage?.DisplayAlert(info.Title, info.Message, info.Cancel);

                    if(task == null)
                        return;  

                    await task;
                    info?.OnCompleted?.Invoke();
                });


            MessagingService.Current.Subscribe<MessagingServiceQuestion>(MessageKeys.Question, async (m, q) =>
                {
                    var task =  Application.Current?.MainPage?.DisplayAlert(q.Title, q.Question, q.Positive, q.Negative);
                    if(task == null)
                        return;
                    var result = await task;
                    q?.OnCompleted?.Invoke(result);
                });

            MessagingService.Current.Subscribe<MessagingServiceChoice>(MessageKeys.Choice, async (m, q) =>
                {
                    var task =  Application.Current?.MainPage?.DisplayActionSheet(q.Title, q.Cancel, q.Destruction, q.Items);
                    if(task == null)
                        return;
                    var result = await task;
                    q?.OnCompleted?.Invoke(result);
                });

            MessagingService.Current.Subscribe(MessageKeys.NavigateLogin, async m =>
                {
                    if(Device.RuntimePlatform == Device.Android)
                    {
                        ((RootPageAndroid)MainPage).IsPresented = false;
                    }

                    Page page = null;
                    if(Settings.Current.FirstRun && Device.RuntimePlatform == Device.Android)
                        page = new LoginPage();
                    else
                        page = new ConferenceNavigationPage(new LoginPage());

                   
                    var nav = Application.Current?.MainPage?.Navigation;
                    if(nav == null)
                        return;
                   
                    await NavigationService.PushModalAsync(nav, page);

                });

            try
            {
                if (firstRun || Device.RuntimePlatform != Device.iOS)
                    return;

                var mainNav = MainPage as NavigationPage;
                if (mainNav == null)
                    return;

                var rootPage = mainNav.CurrentPage as RootPageiOS;
                if (rootPage == null)
                    return;

                var rootNav = rootPage.CurrentPage as NavigationPage;
                if (rootNav == null)
                    return;
                

                var about = rootNav.CurrentPage as AboutPage;
                if (about != null)
                {
                    about.OnResume();
                    return;
                }
                var sessions = rootNav.CurrentPage as SessionsPage;
                if (sessions != null)
                {
                    sessions.OnResume();
                    return;
                }
                var feed = rootNav.CurrentPage as FeedPage;
                if (feed != null)
                {
                    feed.OnResume();
                    return;
                }
            }
            catch
            {
            }
            finally
            {
                firstRun = false;
            }
        }

 

        protected override void OnAppLinkRequestReceived(Uri uri)
        {
            var data = uri.ToString().ToLowerInvariant();
            //only if deep linking
            if (!data.Contains("/session/"))
                return;

            var id = data.Substring(data.LastIndexOf("/", StringComparison.Ordinal) + 1);

            if (!string.IsNullOrWhiteSpace(id))
            {
                MessagingService.Current.SendMessage<DeepLinkPage>("DeepLinkPage", new DeepLinkPage
                {
                    Page = AppPage.Session,
                    Id = id
                });
            }

            base.OnAppLinkRequestReceived(uri);
        }


        protected override void OnSleep()
        {
            if (!registered)
                return;

            registered = false;
            MessagingService.Current.Unsubscribe(MessageKeys.NavigateLogin);
            MessagingService.Current.Unsubscribe<MessagingServiceQuestion>(MessageKeys.Question);
            MessagingService.Current.Unsubscribe<MessagingServiceAlert>(MessageKeys.Message);
            MessagingService.Current.Unsubscribe<MessagingServiceChoice>(MessageKeys.Choice);

            // Handle when your app sleeps
            Connectivity.ConnectivityChanged -= ConnectivityChanged;
        }

        protected async void ConnectivityChanged (ConnectivityChangedEventArgs e)
        {
            //save current state and then set it
            var connected = Settings.Current.IsConnected;
            Settings.Current.IsConnected = e.NetworkAccess == NetworkAccess.Internet;
            if (connected && e.NetworkAccess != NetworkAccess.Internet)
            {
                //we went offline, should alert the user and also update ui (done via settings)
                var task = Application.Current?.MainPage?.DisplayAlert("Offline", "Uh Oh, It looks like you have gone offline. Please check your internet connection to get the latest data and enable syncing data.", "OK");
                if (task != null)
                    await task;
            }
        }
            
    }
}


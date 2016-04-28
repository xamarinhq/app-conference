using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using XamarinEvolve.Clients.UI;
using Xamarin.Forms;
using FormsToolkit.iOS;
using Xamarin.Forms.Platform.iOS;
using Xamarin;
using FormsToolkit;
using XamarinEvolve.Clients.Portable;
using WindowsAzure.Messaging;
using Refractored.XamForms.PullToRefresh.iOS;
using Social;
using CoreSpotlight;
using XamarinEvolve.DataStore.Abstractions;
using HockeyApp;
using System.Threading.Tasks;
using Google.AppIndexing;

namespace XamarinEvolve.iOS
{


    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {

        public static class ShortcutIdentifier
        {
            public const string Tweet = "com.sample.evolve.tweet";
            public const string Announcements = "com.sample.evolve.announcements";
            public const string Events = "com.sample.evolve.events";
            public const string MiniHacks = "com.sample.evolve.minihacks";
        }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            
            var tint = UIColor.FromRGB(118, 53, 235);
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(250, 250, 250); //bar background
            UINavigationBar.Appearance.TintColor = tint; //Tint color of button items

            UIBarButtonItem.Appearance.TintColor = tint; //Tint color of button items

            UITabBar.Appearance.TintColor = tint;

            UISwitch.Appearance.OnTintColor = tint;

            UIAlertView.Appearance.TintColor = tint;

            UIView.AppearanceWhenContainedIn(typeof(UIAlertController)).TintColor = tint;
            UIView.AppearanceWhenContainedIn(typeof(UIActivityViewController)).TintColor = tint;
            UIView.AppearanceWhenContainedIn(typeof(SLComposeViewController)).TintColor = tint;

            #if !ENABLE_TEST_CLOUD
            if (!string.IsNullOrWhiteSpace(ApiKeys.HockeyAppiOS) && ApiKeys.HockeyAppiOS != nameof(ApiKeys.HockeyAppiOS)))
            {
               
                var manager = BITHockeyManager.SharedHockeyManager;
                manager.Configure(ApiKeys.HockeyAppiOS);

                //Disable update manager
                manager.DisableUpdateManager = true;

                manager.StartManager();
                //manager.Authenticator.AuthenticateInstallation();
                   
            }
            #endif

            Forms.Init();
            FormsMaps.Init();
            Toolkit.Init();

            AppIndexing.SharedInstance.RegisterApp (618319027);

            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            // Code for starting up the Xamarin Test Cloud Agent
            #if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
            //Mapping StyleId to iOS Labels
            Forms.ViewInitialized += (object sender, ViewInitializedEventArgs e) =>
            {
                if (null != e.View.StyleId)
                {
                    e.NativeView.AccessibilityIdentifier = e.View.StyleId;
                }
            };
            #endif

            SetMinimumBackgroundFetchInterval();

            //Random Inits for Linking out.
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            SQLitePCL.CurrentPlatform.Init();
            Plugin.Share.ShareImplementation.ExcludedUIActivityTypes = new List<NSString>
            {
                UIActivityType.PostToFacebook,
                UIActivityType.AssignToContact,
                UIActivityType.OpenInIBooks,
                UIActivityType.PostToVimeo,
                UIActivityType.PostToFlickr,
                UIActivityType.SaveToCameraRoll
            };
            ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            NonScrollableListViewRenderer.Initialize();
            SelectedTabPageRenderer.Initialize();
            TextViewValue1Renderer.Init();
            PullToRefreshLayoutRenderer.Init();
            LoadApplication(new App());


           

            // Process any potential notification data from launch
            ProcessNotification(options);

            NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidBecomeActiveNotification, DidBecomeActive);



            return base.FinishedLaunching(app, options);
        }


        void DidBecomeActive(NSNotification notification)
        {
            ((XamarinEvolve.Clients.UI.App)Xamarin.Forms.Application.Current).SecondOnResume();

        }

        public override void WillEnterForeground(UIApplication uiApplication)
        {
            base.WillEnterForeground(uiApplication);
            ((XamarinEvolve.Clients.UI.App)Xamarin.Forms.Application.Current).SecondOnResume();
        }



        public override void RegisteredForRemoteNotifications(UIApplication app, NSData deviceToken)
        {

#if ENABLE_TEST_CLOUD
#else

            if (ApiKeys.AzureServiceBusUrl == nameof(ApiKeys.AzureServiceBusUrl))
                return;

            // Connection string from your azure dashboard
            var cs = SBConnectionString.CreateListenAccess(
                new NSUrl(ApiKeys.AzureServiceBusUrl),
                ApiKeys.AzureKey);

            // Register our info with Azure
            var hub = new SBNotificationHub (cs, ApiKeys.AzureHubName);
            hub.RegisterNativeAsync (deviceToken, null, err => {
                if (err != null)
                    Console.WriteLine("Error: " + err.Description);
                else
                    Console.WriteLine("Success");
            });
            #endif
        }

        public override void ReceivedRemoteNotification(UIApplication app, NSDictionary userInfo)
        {
            // Process a notification received while the app was already open
            ProcessNotification(userInfo);
        }

        void ProcessNotification(NSDictionary userInfo)
        {
            if (userInfo == null)
                return;

            Console.WriteLine("Received Notification");

            var apsKey = new NSString("aps");

            if (userInfo.ContainsKey(apsKey))
            {

                var alertKey = new NSString("alert");

                var aps = (NSDictionary)userInfo.ObjectForKey(apsKey);

                if (aps.ContainsKey(alertKey))
                {
                    var alert = (NSString)aps.ObjectForKey(alertKey);

                    try 
                    {

                        var avAlert = new UIAlertView ("Evolve Update", alert, null, "OK", null);
                        avAlert.Show ();
                      
                    } 
                    catch (Exception ex) 
                    {
                        
                    }

                    Console.WriteLine("Notification: " + alert);
                }
            }
        }

        #region Quick Action

        public UIApplicationShortcutItem LaunchedShortcutItem { get; set; }

        public override void OnActivated(UIApplication application)
        {
            Console.WriteLine("OnActivated");

            // Handle any shortcut item being selected
            HandleShortcutItem(LaunchedShortcutItem);



            // Clear shortcut after it's been handled
            LaunchedShortcutItem = null;
        }
        // if app is already running
        public override void PerformActionForShortcutItem(UIApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler)
        {
            Console.WriteLine("PerformActionForShortcutItem");
            // Perform action
            var handled = HandleShortcutItem(shortcutItem);
            completionHandler(handled);
        }

        public bool HandleShortcutItem(UIApplicationShortcutItem shortcutItem)
        {
            Console.WriteLine("HandleShortcutItem ");
            var handled = false;

            // Anything to process?
            if (shortcutItem == null)
                return false;


            // Take action based on the shortcut type
            switch (shortcutItem.Type)
            {
                case ShortcutIdentifier.Tweet:
                    Console.WriteLine("QUICKACTION: Tweet");
                    var slComposer = SLComposeViewController.FromService(SLServiceType.Twitter);
                    if (slComposer == null)
                    {
                        new UIAlertView("Unavailable", "Twitter is not available, please sign in on your devices settings screen.", null, "OK").Show();
                    }
                    else
                    {
                        slComposer.SetInitialText("#XamarinEvolve");
                        if (slComposer.EditButtonItem != null)
                        {
                            slComposer.EditButtonItem.TintColor = UIColor.FromRGB(118, 53, 235);
                        }
                        slComposer.CompletionHandler += (result) =>
                        {
                            InvokeOnMainThread(() => UIApplication.SharedApplication.KeyWindow.RootViewController.DismissViewController(true, null));
                        };
                        
                        UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewControllerAsync(slComposer, true);
                    }
                    handled = true;
                    break;
                case ShortcutIdentifier.Announcements:
                    Console.WriteLine("QUICKACTION: Accouncements");
                    ContinueNavigation(AppPage.Notification);
                    handled = true;
                    break;
                case ShortcutIdentifier.MiniHacks:
                    Console.WriteLine("QUICKACTION: MiniHacks");
                    ContinueNavigation(AppPage.MiniHacks);
                    handled = true;
                    break;
                case ShortcutIdentifier.Events:
                    Console.WriteLine("QUICKACTION: Events");
                    ContinueNavigation(AppPage.Events);
                    handled = true;
                    break;
            }

            Console.Write(handled);
            // Return results
            return handled;
        }

        void ContinueNavigation(AppPage page, string id = null)
        {
            Console.WriteLine("ContinueNavigation");

            // TODO: display UI in Forms somehow
            System.Console.WriteLine("Show the page for " + page);
            MessagingService.Current.SendMessage<DeepLinkPage>("DeepLinkPage", new DeepLinkPage
                {
                    Page = page,
                    Id = id
                });
        }

        #endregion

        #region Background Refresh

        private void SetMinimumBackgroundFetchInterval()
        {
            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(MINIMUM_BACKGROUND_FETCH_INTERVAL);
        }

        // Minimum number of seconds between a background refresh this is shorter than Android because it is easily killed off.
        // 20 minutes = 20 * 60 = 1200 seconds
        private const double MINIMUM_BACKGROUND_FETCH_INTERVAL = 1200;

        // Called whenever your app performs a background fetch
        public override async void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
        {
            // Do Background Fetch
            var downloadSuccessful = false;
            try
            {
                Xamarin.Forms.Forms.Init();//need for dependency services
                // Download data
                var manager = DependencyService.Get <IStoreManager>();

                downloadSuccessful = await manager.SyncAllAsync(Settings.Current.IsLoggedIn);
            }
            catch (Exception ex)
            {
                var logger = DependencyService.Get <ILogger>();
                ex.Data["Method"] = "PerformFetch";
                logger.Report(ex);
            }

            // If you don't call this, your application will be terminated by the OS.
            // Allows OS to collect stats like data cost and power consumption
            if (downloadSuccessful)
            {
                completionHandler(UIBackgroundFetchResult.NewData);
                Settings.Current.HasSyncedData = true;
                Settings.Current.LastSync = DateTime.UtcNow;
            }
            else
            {
                completionHandler(UIBackgroundFetchResult.Failed);
            }
        }

        #endregion
    }
}


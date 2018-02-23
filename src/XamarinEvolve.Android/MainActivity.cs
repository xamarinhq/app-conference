
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Gcm;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using FormsToolkit;
using FormsToolkit.Droid;
using Plugin.Permissions;
using Refractored.XamForms.PullToRefresh.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinEvolve.Clients.Portable;
using XamarinEvolve.Clients.UI;
using XamarinEvolve.DataObjects;
using Xamarin;
//using Gcm;
//using Gcm.Client;
using XamarinEvolve.Droid.Notifications;

namespace XamarinEvolve.Droid
{
    

    [Activity(Label = "Evolve16", 
        Name="com.sample.evolve.MainActivity",
        Exported = true,
        Icon = "@drawable/newicon", 
        LaunchMode = LaunchMode.SingleTask, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate (Bundle savedInstanceState)
        {
            ToolbarResource = Resource.Layout.toolbar;
            TabLayoutResource = Resource.Layout.tabs;

            base.OnCreate (savedInstanceState);

            Forms.Init (this, savedInstanceState);
            FormsMaps.Init(this, savedInstanceState);
           
            Toolkit.Init ();
            
            PullToRefreshLayoutRenderer.Init ();
            typeof (Color).GetProperty ("Accent", BindingFlags.Public | BindingFlags.Static).SetValue (null, Color.FromHex ("#757575"));

            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init ();

            ZXing.Net.Mobile.Forms.Android.Platform.Init ();
#if ENABLE_TEST_CLOUD
            //Mapping StyleID to element content descriptions
            Xamarin.Forms.Forms.ViewInitialized += (object sender, Xamarin.Forms.ViewInitializedEventArgs e) => {
                if (!string.IsNullOrWhiteSpace(e.View.StyleId)) {
                    e.NativeView.ContentDescription = e.View.StyleId;
                }
            };
#endif


            LoadApplication (new App ());

            var gpsAvailable = IsPlayServicesAvailable ();
            Settings.Current.PushNotificationsEnabled = gpsAvailable;

            OnNewIntent (Intent);


            if(!string.IsNullOrWhiteSpace(Intent?.Data?.LastPathSegment))
            {

                switch(Intent.Data.LastPathSegment)
                {
                    case "sessions":
                        MessagingService.Current.SendMessage<DeepLinkPage>("DeepLinkPage", new DeepLinkPage
                        {
                            Page = AppPage.Sessions
                        });
                        break;
                    case "events":
                        MessagingService.Current.SendMessage<DeepLinkPage>("DeepLinkPage", new DeepLinkPage
                        {
                            Page = AppPage.Events
                        });
                        break;
                    case "minihacks":
                        MessagingService.Current.SendMessage<DeepLinkPage>("DeepLinkPage", new DeepLinkPage
                        {
                            Page = AppPage.MiniHacks
                        });
                        break;
                }
            }

            if (!Settings.Current.PushNotificationsEnabled)
                return;
#if ENABLE_TEST_CLOUD
#else
            RegisterWithGCM ();
#endif

            DataRefreshService.ScheduleRefresh (this);
        }



        
        private void RegisterWithGCM()
        {
            // Check to ensure everything's set up right
            //GcmClient.CheckDevice(this);
            //GcmClient.CheckManifest(this);

            EvolveRegistrationService.Register(this);

            // Register for push notifications
            //System.Diagnostics.Debug.WriteLine("MainActivity", "Registering...");
            //GcmService.Initialize(this);
            //GcmService.Register(this);
        }

        public bool IsPlayServicesAvailable ()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable (this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    if (Settings.Current.GooglePlayChecked)
                        return false;

                    Settings.Current.GooglePlayChecked = true;
                    Toast.MakeText(this, "Google Play services is not installed, push notifications have been disabled.", ToastLength.Long).Show();
                }
                else
                {
                    Settings.Current.PushNotificationsEnabled = false;
                }
                return false;
            }
            else
            {
                Settings.Current.PushNotificationsEnabled = true;
                return true;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult (requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}


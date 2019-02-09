using System;
using Xamarin.Forms.Platform.Android;
using Android.Support.Design.Widget;
using Android.Runtime;
using Xamarin.Forms;
using Conference.Droid;
using Conference.Clients.Portable;
using Android.Widget;
using FormsToolkit;
using Android.Views;

[assembly: ExportRenderer (typeof(Conference.Clients.UI.NavigationView), typeof(NavigationViewRenderer))]
namespace Conference.Droid
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class NavigationViewRenderer : ViewRenderer<Conference.Clients.UI.NavigationView, NavigationView>
    {
        NavigationView navView;
        ImageView profileImage;
        TextView profileName;
        protected override void OnElementChanged(ElementChangedEventArgs<Conference.Clients.UI.NavigationView> e)
        {
            
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;


            var view = Inflate(Forms.Context, Resource.Layout.nav_view, null);
            navView = view.JavaCast<NavigationView>();


            navView.NavigationItemSelected += NavView_NavigationItemSelected;

            Settings.Current.PropertyChanged += SettingsPropertyChanged;
            SetNativeControl(navView);

            var header = navView.GetHeaderView(0);
            profileImage = header.FindViewById<ImageView>(Resource.Id.profile_image);
            profileName = header.FindViewById<TextView>(Resource.Id.profile_name);

            profileImage.Click += (sender, e2) => NavigateToLogin();
            profileName.Click += (sender, e2) => NavigateToLogin();

            UpdateName();
            UpdateImage();

            navView.SetCheckedItem(Resource.Id.nav_feed);
        }

        void NavigateToLogin()
        {
            if (Settings.Current.IsLoggedIn)
                return;

            Conference.Clients.UI.App.Logger.TrackPage(AppPage.Login.ToString(), "navigation");
            MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
        }

        void SettingsPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Settings.Current.Email))
            {
                UpdateName();
                UpdateImage();
            }
        }

        void UpdateName()
        {
            profileName.Text = Settings.Current.UserDisplayName;
        }

        void UpdateImage()
        {
            Android.Glide.Glide.With(Forms.Context)
               .Load(Settings.Current.UserAvatar)
               //.Error(Resource.Drawable.profile_generic)
               .Into(profileImage);

        }

        public override void OnViewRemoved(Android.Views.View child)
        {
            base.OnViewRemoved(child);
            navView.NavigationItemSelected -= NavView_NavigationItemSelected;
            Settings.Current.PropertyChanged -= SettingsPropertyChanged;
        }

        IMenuItem previousItem;

        void NavView_NavigationItemSelected (object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {


            if (previousItem != null )
                previousItem.SetChecked(false);

            navView.SetCheckedItem(e.MenuItem.ItemId);

            previousItem = e.MenuItem;

            var id = 0;
            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.nav_feed:
                    id = (int)AppPage.Feed;
                    break;
                case Resource.Id.nav_sessions:
                    id = (int)AppPage.Sessions;
                    break;
                case Resource.Id.nav_events:
                    id = (int)AppPage.Events;
                    break;
                case Resource.Id.nav_sponsors:
                    id = (int)AppPage.Sponsors;
                    break;
                case Resource.Id.nav_venue:
                    id = (int)AppPage.Venue;
                    break;
                case Resource.Id.nav_floor_map:
                    id = (int)AppPage.FloorMap;
                    break;
                case Resource.Id.nav_conference_info:
                    id = (int)AppPage.ConferenceInfo;
                    break;
                case Resource.Id.nav_mini_hacks:
                    id = (int)AppPage.MiniHacks;
                    break;
                case Resource.Id.nav_settings:
                    id = (int)AppPage.Settings;
                    break;
                case Resource.Id.nav_evals:
                    id = (int)AppPage.Evals;
                    break;
            }
            this.Element.OnNavigationItemSelected(new Conference.Clients.UI.NavigationItemSelectedEventArgs
                {
                   
                    Index = id
                });
        }


    }
#pragma warning restore CS0618 // Type or member is obsolete
}


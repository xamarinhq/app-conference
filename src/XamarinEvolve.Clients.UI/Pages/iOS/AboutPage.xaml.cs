using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinEvolve.Clients.Portable;
using FormsToolkit;

namespace XamarinEvolve.Clients.UI
{
    public partial class AboutPage : ContentPage
    {
        AboutViewModel vm;
        IPushNotifications push;
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = vm = new AboutViewModel();
            push = DependencyService.Get<IPushNotifications>();
            var adjust = Device.RuntimePlatform != Device.Android ? 1 : -vm.AboutItems.Count + 1;
            ListViewAbout.HeightRequest = (vm.AboutItems.Count * ListViewAbout.RowHeight) - adjust;
            ListViewAbout.ItemTapped += (sender, e) => ListViewAbout.SelectedItem = null;
            ListViewInfo.HeightRequest = (vm.InfoItems.Count * ListViewInfo.RowHeight) - adjust;

            ListViewAccount.HeightRequest = (vm.AccountItems.Count * ListViewAccount.RowHeight) - adjust;
            ListViewAccount.ItemTapped += (sender, e) => ListViewAccount.SelectedItem = null;;

            ListViewAbout.ItemSelected += async (sender, e) => 
                {
                    if(ListViewAbout.SelectedItem == null)
                        return;

                    App.Logger.TrackPage(AppPage.Settings.ToString());
                    await NavigationService.PushAsync(Navigation, new SettingsPage());

                    ListViewAbout.SelectedItem = null;
                };

            ListViewInfo.ItemSelected += async (sender, e) => 
                {
                    var item = ListViewInfo.SelectedItem as XamarinEvolve.Clients.Portable.MenuItem;
                    if(item == null)
                        return;
                    Page page = null;
                    switch(item.Parameter)
                    {
                        case "evaluations":
                            App.Logger.TrackPage ("Evaluations");
                            page = new EvaluationsPage ();
                            break;
                        case "venue":
                            App.Logger.TrackPage(AppPage.Venue.ToString());
                            page = new VenuePage();
                            break;
                        case "code-of-conduct":
                            App.Logger.TrackPage(AppPage.CodeOfConduct.ToString());
                            page = new CodeOfConductPage();
                            break;
                        case "wi-fi":
                            App.Logger.TrackPage(AppPage.WiFi.ToString());
                            page = new WiFiInformationPage();
                            break;
                        case "sponsors":
                            App.Logger.TrackPage(AppPage.Sponsors.ToString());
                            page = new SponsorsPage();
                            break;
                        case "floor-maps":
                            App.Logger.TrackPage(AppPage.FloorMap.ToString());
                            page = new FloorMapsPage();
                            break;
                    }

                    if(page == null)
                        return;
                    if(Device.RuntimePlatform == Device.iOS && page is VenuePage)
                        await NavigationService.PushAsync(((Page)this.Parent.Parent).Navigation, page);
                    else
                        await NavigationService.PushAsync(Navigation, page);

                    ListViewInfo.SelectedItem = null;
                };
            isRegistered = push.IsRegistered;
        }

        bool isRegistered;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!isRegistered && Settings.Current.AttemptedPush)
            {
                push.RegisterForNotifications();
            }
            isRegistered = push.IsRegistered;
            vm.UpdateItems();
        }

        public void OnResume()
        {
            OnAppearing();
        }
    }
}


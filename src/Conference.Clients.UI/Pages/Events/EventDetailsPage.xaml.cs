using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Conference.Clients.Portable;
using Conference.DataObjects;
using FormsToolkit;

namespace Conference.Clients.UI
{
    public partial class EventDetailsPage : ContentPage
    {
        EventDetailsViewModel ViewModel => vm ?? (vm = BindingContext as EventDetailsViewModel);
        EventDetailsViewModel vm;

        public EventDetailsPage()
        {
            InitializeComponent();

            ListViewSponsors.ItemSelected += async (sender, e) => 
                {
                    var sponsor = ListViewSponsors.SelectedItem as Sponsor;
                    if(sponsor == null)
                        return;

                    var sponsorDetails = new SponsorDetailsPage
                        {
                            Sponsor = sponsor
                        };

                    App.Logger.TrackPage(AppPage.Sponsor.ToString(), sponsor.Name);
                    await NavigationService.PushAsync(Navigation, sponsorDetails);

                    ListViewSponsors.SelectedItem = null;
                };
        }

        public FeaturedEvent Event
        {
            get { return ViewModel.Event; }
            set { BindingContext = new EventDetailsViewModel(Navigation, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            vm = null;

            var adjust = Device.RuntimePlatform != Device.Android ? 1 : -ViewModel.Sponsors.Count + 1;
            ListViewSponsors.HeightRequest = ListViewSponsors.RowHeight - adjust;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing(); 

            ViewModel.LoadEventDetailsCommand.Execute(null);

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}


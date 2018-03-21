using System;
using Xamarin.Forms;
using Conference.DataObjects;
using FormsToolkit;
using Conference.Clients.Portable;

namespace Conference.Clients.UI
{
    public partial class SponsorsPage : ContentPage
    {
        SponsorsViewModel vm;
        SponsorsViewModel ViewModel => vm ?? (vm = BindingContext as SponsorsViewModel); 

        public SponsorsPage()
        {
            InitializeComponent();
            BindingContext = new SponsorsViewModel(Navigation);

            if (Device.RuntimePlatform == Device.Android)
                ListViewSponsors.Effects.Add (Effect.Resolve ("Xamarin.ListViewSelectionOnTopEffect"));

            if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.WinPhone)
            {
                ToolbarItems.Add(new ToolbarItem
                {
                    Text = "Refresh",
                    Icon = "toolbar_refresh.png",
                    Command = ViewModel.ForceRefreshCommand
                });
            }
            ListViewSponsors.ItemSelected += async (sender, e) => 
            {
                    var sponsor = ListViewSponsors.SelectedItem as Sponsor;
                    if(sponsor == null)
                        return;
                    var sponsorDetails = new SponsorDetailsPage();

                    sponsorDetails.Sponsor = sponsor;
                    App.Logger.TrackPage(AppPage.Sponsor.ToString(), sponsor.Name);
                    await NavigationService.PushAsync(Navigation, sponsorDetails);
                    ListViewSponsors.SelectedItem = null;
            };
        }

        void ListViewTapped (object sender, ItemTappedEventArgs e)
        {
            var list = sender as ListView;
            if (list == null)
                return;
            list.SelectedItem = null;
        }
            
        protected override void OnAppearing()
        {
            base.OnAppearing();

            ListViewSponsors.ItemTapped += ListViewTapped;
            if (ViewModel.Sponsors.Count == 0)
                ViewModel.LoadSponsorsCommand.Execute(false);

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ListViewSponsors.ItemTapped -= ListViewTapped;
        }
    }
}


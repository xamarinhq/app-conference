using System;
using Xamarin.Forms;
using Conference.DataObjects;
using FormsToolkit;
using MvvmHelpers;

namespace Conference.Clients.Portable
{
    public class SponsorDetailsViewModel : ViewModelBase
    {
        
        public Sponsor Sponsor { get; }
        public ObservableRangeCollection<MenuItem> FollowItems { get; } = new ObservableRangeCollection<MenuItem>();

        public SponsorDetailsViewModel(INavigation navigation, Sponsor sponsor) : base(navigation)
        {
            Sponsor = sponsor;
            FollowItems.Add(new MenuItem
                {
                    Name = "Web",
                    Subtitle = sponsor.WebsiteUrl,
                    Parameter = sponsor.WebsiteUrl,
                    Icon = "icon_website.png"
                });
            FollowItems.Add(new MenuItem
                {
                    Name = Device.RuntimePlatform == Device.iOS ? "Twitter" : sponsor.TwitterUrl,
                    Subtitle = $"@{sponsor.TwitterUrl}",
                    Parameter = "http://twitter.com/" + sponsor.TwitterUrl,
                    Icon = "icon_twitter.png"
                });
        }

        MenuItem selectedFollowItem;
        public MenuItem SelectedFollowItem
        {
            get => selectedFollowItem;
            set
            {
                selectedFollowItem = value;
                OnPropertyChanged();
                if (selectedFollowItem == null)
                    return;

                LaunchBrowserCommand.Execute(selectedFollowItem.Parameter);

                SelectedFollowItem = null;
            }
        }
    }
}


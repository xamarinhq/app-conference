using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Diagnostics;
using XamarinEvolve.Clients.Portable;

namespace XamarinEvolve.Clients.UI
{
    public partial class TweetImagePage : ContentPage
    {
        public TweetImagePage(string image)
        {
            InitializeComponent();
            var item = new ToolbarItem
            {
                Text = "Done",
                Command = new Command(async () => await Navigation.PopModalAsync())
            };
            if (Device.OS == TargetPlatform.Android)
                item.Icon = "toolbar_close.png";
            ToolbarItems.Add(item);

            try
            {
                MainImage.Source = new UriImageSource
                {
                    Uri = new Uri(image),
                    CachingEnabled = true,
                    CacheValidity = TimeSpan.FromDays(3)
                };
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Unable to convert image to URI: " + ex);
                DependencyService.Get<IToast>().SendToast("Unable to load image.");
            }



            MainImage.PropertyChanged += (sender, e) => 
                {
                    if(e.PropertyName != nameof(MainImage.IsLoading))
                        return;
                    ProgressBar.IsRunning = MainImage.IsLoading;
                    ProgressBar.IsVisible = MainImage.IsLoading;
                };
        }
    }
}


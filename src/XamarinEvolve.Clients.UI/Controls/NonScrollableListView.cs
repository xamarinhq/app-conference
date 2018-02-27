using System;
using Xamarin.Forms;

namespace XamarinEvolve.Clients.UI
{
    public class NonScrollableListView : ListView
    {
        public NonScrollableListView()
            :base(ListViewCachingStrategy.RecycleElement)
        {
            if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.WinPhone)
                BackgroundColor = Color.White;
        }
    }
}


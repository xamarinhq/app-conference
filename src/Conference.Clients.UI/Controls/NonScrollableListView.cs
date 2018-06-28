using System;
using Xamarin.Forms;

namespace Conference.Clients.UI
{
    public class NonScrollableListView : ListView
    {
        public NonScrollableListView()
            :base(ListViewCachingStrategy.RecycleElement)
        {
            if (Device.RuntimePlatform == Device.UWP)
                BackgroundColor = Color.White;
        }
    }
}


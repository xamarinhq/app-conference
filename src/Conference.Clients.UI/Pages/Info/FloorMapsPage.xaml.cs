using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Conference.Clients.UI
{
    public partial class FloorMapsPage : CarouselPage
    {
        public FloorMapsPage()
        {
            InitializeComponent();

            var items = new List<ConferenceMap>
            {
                new ConferenceMap
                {
                    Local = "floor_1.png",
                    Url = "https://s3.amazonaws.com/xamarin-releases/conference-2016/floor_1.png",
                    Title = "Floor Maps (1/2)"
                },
                new ConferenceMap
                {
                    Local = "floor_2.png",
                    Url = "https://s3.amazonaws.com/xamarin-releases/conference-2016/floor_2.png",
                    Title = "Floor Maps (2/2)"
                }
            };

            Map1.BindingContext = items[0];
            Map2.BindingContext = items[1];



            if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
            {
                
                Title = "Floor Maps (1/2)";
                this.PagesChanged += (sender, args) =>
                {
                    var current = this.CurrentPage.BindingContext as ConferenceMap;
                    if (current == null)
                        return;
                    Title = current.Title;
                };
            }
        }
    }
}

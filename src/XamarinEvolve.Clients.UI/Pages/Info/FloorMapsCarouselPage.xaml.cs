using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinEvolve.Clients.UI
{
    public partial class FloorMapsCarouselPage : CarouselPage
    {
        public FloorMapsCarouselPage()
        {
            InitializeComponent();

            Children.Add(new FloorMapPage(0));
            Children.Add(new FloorMapPage(1));

            if (Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.iOS)
            {
                Title = Children[0].Title;
                CurrentPageChanged += (sender, e) =>
                {
                    Title = CurrentPage.Title;
                };
            }
        }
    }
}

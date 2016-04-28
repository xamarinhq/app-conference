using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinEvolve.Clients.UI
{
    public partial class FloorMapCell : ContentView
    {
        public FloorMapCell()
        {
            InitializeComponent();
            MainImage.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName != nameof(MainImage.IsLoading))
                    return;
                ProgressBar.IsRunning = MainImage.IsLoading;
                ProgressBar.IsVisible = MainImage.IsLoading;
            };
        }
    }
}

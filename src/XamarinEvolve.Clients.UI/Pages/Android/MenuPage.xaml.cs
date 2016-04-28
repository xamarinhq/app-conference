using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace XamarinEvolve.Clients.UI
{
    public partial class MenuPage : ContentPage
    {
        RootPageAndroid root;
        public MenuPage(RootPageAndroid root)
        {
            this.root = root;
            InitializeComponent();

            NavView.NavigationItemSelected += (sender, e) =>
            {
                this.root.IsPresented = false;
                Device.StartTimer(TimeSpan.FromMilliseconds(300), ()=>
                {
                        Device.BeginInvokeOnMainThread(async () => 
                            {
                                await this.root.NavigateAsync(e.Index);
                            });
                    return false;
                });
            };
        }
    }
}


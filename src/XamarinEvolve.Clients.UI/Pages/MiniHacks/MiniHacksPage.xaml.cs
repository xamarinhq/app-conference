using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinEvolve.Clients.Portable;
using XamarinEvolve.DataObjects;

namespace XamarinEvolve.Clients.UI
{
    public partial class MiniHacksPage : ContentPage
    {
        MiniHacksViewModel vm;
        public MiniHacksPage()
        {
            InitializeComponent();
            BindingContext = vm = new MiniHacksViewModel();
            if (Device.RuntimePlatform == Device.Android)
                ListViewMiniHacks.Effects.Add (Effect.Resolve ("Xamarin.ListViewSelectionOnTopEffect"));

            if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.WinPhone)
            {
                ToolbarItems.Add(new ToolbarItem
                {
                    Text = "Refresh",
                    Icon = "toolbar_refresh.png",
                    Command = vm.ForceRefreshCommand
                });
            }

            ListViewMiniHacks.ItemTapped += (sender, e) => ListViewMiniHacks.SelectedItem = null;
            ListViewMiniHacks.ItemSelected += async (sender, e) => 
                {
                    var hack = ListViewMiniHacks.SelectedItem as MiniHack;
                    if(hack == null)
                        return;

                    await NavigationService.PushAsync(Navigation, new MiniHacksDetailsPage(hack));


                    ListViewMiniHacks.SelectedItem = null;
                };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (vm.MiniHacks.Count == 0)
                vm.LoadMiniHacksCommand.Execute(false);
        }
    }
}


using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinEvolve.Clients.Portable;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace XamarinEvolve.Clients.UI
{
    public partial class VenuePage : ContentPage
    {
        VenueViewModel vm;
        public VenuePage()
        {
            InitializeComponent();
            BindingContext = vm = new VenueViewModel();

            if (Device.RuntimePlatform == Device.Android)
            {
                ToolbarItems.Add(new ToolbarItem
                    {
                        Order = ToolbarItemOrder.Secondary,
                        Text = "Get Directions",
                        Command = vm.NavigateCommand

                    });

                if (vm.CanMakePhoneCall)
                {

                    ToolbarItems.Add(new ToolbarItem
                    {
                        Order = ToolbarItemOrder.Secondary,
                        Text = "Call Hotel",
                        Command = vm.CallCommand
                    });
                }
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                ToolbarItems.Add(new ToolbarItem
                    {
                        Text = "More",
                        Icon = "toolbar_overflow.png",
                        Command = new Command(async () =>
                            {
                                string[] items = null;
                                if (!vm.CanMakePhoneCall)
                                {
                                    items = new[] { "Get Directions" };
                                }
                                else
                                {
                                    items = new[] { "Get Directions", "Call +1 (407) 284-1234" };
                                }
                                var action = await DisplayActionSheet("Hyatt Regency", "Cancel", null, items);
                                if (action == items[0])
                                    vm.NavigateCommand.Execute(null);
                                else if (items.Length > 1 && action == items[1] && vm.CanMakePhoneCall)
                                    vm.CallCommand.Execute(null);

                            })
                    });
            }
            else
            {
                ToolbarItems.Add(new ToolbarItem
                {
                    Text = "Directions",
                    Command = vm.NavigateCommand,
                    Icon = "toolbar_navigate.png"
                });

                if (vm.CanMakePhoneCall)
                {

                    ToolbarItems.Add(new ToolbarItem
                    {
                        Text = "Call",
                        Command = vm.CallCommand,
                        Icon = "toolbar_call.png"
                    });
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (EvolveMap.Pins.Count > 0)
                return;

            var position = new Position(vm.Latitude, vm.Longitude);
            EvolveMap.MoveToRegion(new MapSpan(position, 0.02, 0.02));
            EvolveMap.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Address = vm.LocationTitle,
                Label = vm.EventTitle,
                Position = position
            });
        }

       
    }
}


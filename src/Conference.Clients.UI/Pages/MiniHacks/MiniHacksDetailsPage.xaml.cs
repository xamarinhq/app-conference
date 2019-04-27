using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Conference.DataObjects;
using Conference.Clients.Portable;
using ZXing.Net.Mobile.Forms;
using ZXing.Mobile;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using FormsToolkit;

namespace Conference.Clients.UI
{
    public partial class MiniHacksDetailsPage : ContentPage
    {
        MiniHackDetailsViewModel vm;
        ZXingScannerPage scanPage;
        public MiniHacksDetailsPage(MiniHack hack)
        {
            InitializeComponent();
            BindingContext = vm = new MiniHackDetailsViewModel(hack);

            ButtonFinish.Clicked += ButtonFinish_Clicked;


            if (string.IsNullOrWhiteSpace (hack.GitHubUrl)) 
            {
                MiniHackDirections1.IsEnabled = false;
                MiniHackDirections1.Text = "Directions coming soon";
                MiniHackDirections2.IsEnabled = false;
                MiniHackDirections2.Text = "Directions coming soon";
            }

            scanPage = new ZXingScannerPage(new MobileBarcodeScanningOptions {  AutoRotate = false, })
                {
                    DefaultOverlayTopText = "Align the barcode within the frame",
                    DefaultOverlayBottomText = string.Empty
                };

            scanPage.OnScanResult += ScanPage_OnScanResult;


            scanPage.Title = "Scan Code";


            var item = new ToolbarItem
                {
                    Text = "Cancel",
                    Command = new Command(async () => 
                        {
                            scanPage.IsScanning = false;
                            await Navigation.PopAsync();
                        })
                };

            if(Device.RuntimePlatform != Device.iOS)
                item.Icon = "toolbar_close.png";

            scanPage.ToolbarItems.Add(item);

        }

        async void ButtonFinish_Clicked (object sender, EventArgs e)
        {
            
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var request = false;
            if(status == PermissionStatus.Denied)
            {
                if(Device.RuntimePlatform == Device.iOS)
                {
                    MessagingService.Current.SendMessage<MessagingServiceQuestion>(MessageKeys.Question, new MessagingServiceQuestion
                        {
                            Title = "Camera Permission",
                            Question = "To finish mini-hacks you will need to scan a code and the camera permission is required. Please go into Settings and turn on Camera for Conference16.",
                            Positive = "Settings",
                            Negative = "Maybe Later",
                            OnCompleted = (result) =>
                                {
                                    if(result)
                                    {
                                        DependencyService.Get<IPushNotifications>().OpenSettings();
                                    }
                                }
                        });
                    return;
                }
                else
                {
                    request = true;
                }
            }

            if(request || status != PermissionStatus.Granted)
            {
                var newStatus = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                if(newStatus.ContainsKey(Permission.Camera) && newStatus[Permission.Camera] != PermissionStatus.Granted)
                {
                    MessagingService.Current.SendMessage<MessagingServiceQuestion>(MessageKeys.Question, new MessagingServiceQuestion
                        {
                            Title = "Camera Permission",
                            Question = "To finish mini-hacks you will need to scan a code and the camera permission is required.",
                            Positive = "Settings",
                            Negative = "Maybe Later",
                            OnCompleted = (result) =>
                                {
                                    if(result)
                                    {
                                        DependencyService.Get<IPushNotifications>().OpenSettings();
                                    }
                                }
                        });
                    return;
                }
            }




            await Navigation.PushAsync (scanPage);
        }

        void ScanPage_OnScanResult (ZXing.Result result)
        {
            // Stop scanning
            scanPage.IsScanning = false;
            // Pop the page and show the result
            Device.BeginInvokeOnMainThread (async () => {

                    await Navigation.PopAsync ();        

                     //admin app will append on conference to the end to it is unique.
                    if(vm.Hack.UnlockCode + "conference" == result.Text)
                    {
                        vm.FinishHack ();
                        App.Logger.Track ("FinishedHack", "Name", vm.Hack.Name);
                        await DisplayAlert("Congratulations", "You successfully finished the Mini-Hack!", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Mini-Hack Issue", "That doesn't seem to be the right code. Please see a Xamarin to help finish the Mini-Hack.", "OK");
                    }

                });
        }
    }
}


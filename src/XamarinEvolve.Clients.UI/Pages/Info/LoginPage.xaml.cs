using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinEvolve.Clients.Portable;
using System.Text.RegularExpressions;

namespace XamarinEvolve.Clients.UI
{
    public partial class LoginPage : ContentPage
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        ImageSource placeholder;
        LoginViewModel vm;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = vm = new LoginViewModel(Navigation);

            if (!Settings.Current.FirstRun)
            {
                Title = "My Account";
                var cancel = new ToolbarItem
                {
                    Text = "Cancel",
                    Command = new Command(async() => 
                            {
                                if(vm.IsBusy)
                                    return;
                                await Navigation.PopModalAsync();
                            })
                };
                ToolbarItems.Add(cancel);

                if (Device.OS != TargetPlatform.iOS)
                    cancel.Icon = "toolbar_close.png";
            }
            
            CircleImageAvatar.Source = placeholder = ImageSource.FromFile("profile_generic_big.png");
            EntryEmail.TextChanged += (sender, e) => 
                {
                    var isValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
                    if(isValid)
                    {
                        Device.BeginInvokeOnMainThread(()=>
                            {
                                CircleImageAvatar.BorderThickness = 3;
                                CircleImageAvatar.Source = ImageSource.FromUri(new Uri(Gravatar.GetURL(EntryEmail.Text)));
                            });

                    }
                    else if(CircleImageAvatar.Source != placeholder)
                    {
                        Device.BeginInvokeOnMainThread(()=>
                            {
                                CircleImageAvatar.BorderThickness = 0;
                                CircleImageAvatar.Source = placeholder;
                            });
                    }
                };
        }

        protected override bool OnBackButtonPressed()
        {
            if(Settings.Current.FirstRun)
                return true;

            return base.OnBackButtonPressed();
        }
    }
}


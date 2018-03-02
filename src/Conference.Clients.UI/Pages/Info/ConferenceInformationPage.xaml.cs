using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Conference.Clients.Portable;

namespace Conference.Clients.UI
{
    public partial class ConferenceInformationPage : ContentPage
    {
        ConferenceInfoViewModel vm; 
        public ConferenceInformationPage()
        {
            InitializeComponent();
            BindingContext = vm = new ConferenceInfoViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            CodeOfConductText.Text = CodeOfConductPage.Conduct;
            await vm.UpdateConfigs();
        }
    }
}


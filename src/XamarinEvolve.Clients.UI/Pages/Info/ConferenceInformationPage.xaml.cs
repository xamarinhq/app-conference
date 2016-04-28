using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinEvolve.Clients.Portable;

namespace XamarinEvolve.Clients.UI
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


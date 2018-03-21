using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Conference.DataObjects;
using Conference.Clients.Portable;

namespace Conference.Clients.UI
{
    public partial class FeedbackPage : ContentPage
    {
        FeedbackViewModel vm;
        public FeedbackPage(Session session)
        {
            InitializeComponent();

            BindingContext = vm = new FeedbackViewModel(Navigation, session);
            if (Device.RuntimePlatform != Device.iOS)
                ToolbarDone.Icon = "toolbar_close.png";


            ToolbarDone.Command = new Command(async () => 
                {
                    if(vm.IsBusy)
                        return;
                    
                    await Navigation.PopModalAsync();
                });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            var items = StarGrid.Behaviors.Count;
            for(int i = 0; i < items; i++)
                StarGrid.Behaviors.RemoveAt(i);
        }
    }
}


using System;
using System.Collections.Generic;
using FormsToolkit;
using Xamarin.Forms;
using XamarinEvolve.Clients.Portable;
using XamarinEvolve.DataObjects;

namespace XamarinEvolve.Clients.UI
{
    public partial class EvaluationsPage : ContentPage
    {
        EvaluationsViewModel viewModel;
        public EvaluationsPage ()
        {
            InitializeComponent ();
            BindingContext = viewModel = new EvaluationsViewModel (Navigation);
            ListViewSessions.ItemTapped += (sender, e) => ListViewSessions.SelectedItem = null;
            ListViewSessions.ItemSelected += async (sender, e) => 
            {
                var session = ListViewSessions.SelectedItem as Session;
                if (session == null)
                    return;


                if (!Settings.Current.IsLoggedIn) 
                {
                    DependencyService.Get<ILogger> ().TrackPage (AppPage.Login.ToString (), "Feedback");
                    MessagingService.Current.SendMessage (MessageKeys.NavigateLogin);
                    return;
                }
                await NavigationService.PushModalAsync (Navigation, new EvolveNavigationPage (new FeedbackPage (session)));
                ListViewSessions.SelectedItem = null;
            };
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            if (Device.OS == TargetPlatform.Android)
                MessagingService.Current.Subscribe ("eval_finished", (d) => UpdatePage ());


            UpdatePage ();
        }

        void UpdatePage ()
        {
            //Load if none, or if 45 minutes has gone by
            if ((viewModel?.Sessions?.Count ?? 0) == 0 || EvaluationsViewModel.ForceRefresh) {
                viewModel?.LoadSessionsCommand?.Execute (null);
            }

            EvaluationsViewModel.ForceRefresh = false;
        }

        protected override void OnDisappearing ()
        {
            base.OnDisappearing ();

            if (Device.OS == TargetPlatform.Android)
                MessagingService.Current.Unsubscribe ("eval_finished");
        }
    }
}


﻿using System;
using Xamarin.Forms;
using Conference.DataObjects;
using FormsToolkit;
using Conference.Clients.Portable;

namespace Conference.Clients.UI
{
    public partial class SessionsPage : ContentPage
    {
        SessionsViewModel ViewModel => vm ?? (vm = BindingContext as SessionsViewModel);
        SessionsViewModel vm;
        bool showFavs, showPast, showAllCategories;
        string filteredCategories;
        ToolbarItem filterItem;
        string loggedIn;
        public SessionsPage()
        {
           
            InitializeComponent();
            loggedIn = Settings.Current.Email;
            showFavs = Settings.Current.FavoritesOnly;
            showPast = Settings.Current.ShowPastSessions;
            showAllCategories = Settings.Current.ShowAllCategories;
            filteredCategories = Settings.Current.FilteredCategories;

            BindingContext = vm = new SessionsViewModel(Navigation);

            if (Device.RuntimePlatform == Device.UWP)
            {
                ToolbarItems.Add(new ToolbarItem
                {
                    Text = "Refresh",
                    Icon = "toolbar_refresh.png",
                    Command = vm.ForceRefreshCommand
                });
            }


            filterItem = new ToolbarItem
            {
                    Text = "Filter"
            };

            if (Device.RuntimePlatform != Device.iOS)
                filterItem.Icon = "toolbar_filter.png";

            filterItem.Command = new Command(async () => 
                {
                    if (vm.IsBusy)
                        return;
                    App.Logger.TrackPage(AppPage.Filter.ToString());
                    await NavigationService.PushModalAsync(Navigation, new ConferenceNavigationPage(new FilterSessionsPage()));
                });

            ToolbarItems.Add(filterItem);

            ListViewSessions.ItemSelected += async (sender, e) => 
                {
                    var session = ListViewSessions.SelectedItem as Session;
                    if(session == null)
                        return;
                    
                    var sessionDetails = new SessionDetailsPage(session);

                    App.Logger.TrackPage(AppPage.Session.ToString(), session.Title);
                    await NavigationService.PushAsync(Navigation, sessionDetails);
                    ListViewSessions.SelectedItem = null;
                };
        }

        void ListViewTapped (object sender, ItemTappedEventArgs e)
        {
            var list = sender as ListView;
            if (list == null)
                return;
            list.SelectedItem = null;
        }
       
        protected override void OnAppearing()
        {
            base.OnAppearing();

            ListViewSessions.ItemTapped += ListViewTapped;

            if (Device.RuntimePlatform == Device.Android)
                MessagingService.Current.Subscribe("filter_changed", (d) => UpdatePage());
            
            UpdatePage();

        }

        void UpdatePage()
        {
            Title = Settings.Current.FavoritesOnly ? "Favorite Sessions" : "Sessions";

            var forceRefresh = (DateTime.UtcNow > (ViewModel?.NextForceRefresh ?? DateTime.UtcNow)) ||
                loggedIn != Settings.Current.Email;

            loggedIn = Settings.Current.Email;
            //Load if none, or if 45 minutes has gone by
            if ((ViewModel?.Sessions?.Count ?? 0) == 0 || forceRefresh)
            {
                ViewModel?.LoadSessionsCommand?.Execute(forceRefresh);
            }
            else if (showFavs != Settings.Current.FavoritesOnly ||
                    showPast != Settings.Current.ShowPastSessions ||
                    showAllCategories != Settings.Current.ShowAllCategories ||
                    filteredCategories != Settings.Current.FilteredCategories)
            {
                showFavs = Settings.Current.FavoritesOnly;
                showPast = Settings.Current.ShowPastSessions;
                showAllCategories = Settings.Current.ShowAllCategories;
                filteredCategories = Settings.Current.FilteredCategories;
                ViewModel?.FilterSessionsCommand?.Execute(null);
            }
        }

        protected override void OnDisappearing()

        {
            base.OnDisappearing();
            ListViewSessions.ItemTapped -= ListViewTapped;
            if (Device.RuntimePlatform == Device.Android)
                MessagingService.Current.Unsubscribe("filter_changed");
        }

        public void OnResume()
        {
            UpdatePage();
        }
    }
}


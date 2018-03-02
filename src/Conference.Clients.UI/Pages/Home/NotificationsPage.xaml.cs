using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Conference.Clients.Portable;

namespace Conference.Clients.UI
{
    public partial class NotificationsPage : ContentPage
    {
        NotificationsViewModel vm;
        public NotificationsPage()
        {
            InitializeComponent();
            BindingContext = vm = new NotificationsViewModel();
            ListViewNotifications.ItemTapped += (sender, e) => ListViewNotifications.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (vm.Notifications.Count == 0)
                vm.LoadNotificationsCommand.Execute(false);
        }
    }
}


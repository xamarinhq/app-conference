using Xamarin.Forms;
using Conference.Clients.Portable;
using Conference.DataObjects;
using System;
using FormsToolkit;

namespace Conference.Clients.UI
{
    public partial class FilterSessionsPage : ContentPage
    {
        FilterSessionsViewModel vm;
        Category showFavorites, showPast;
        public FilterSessionsPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform != Device.iOS)
                ToolbarDone.Icon = "toolbar_close.png";

            ToolbarDone.Command = new Command (async () =>
            {
                Settings.Current.FavoritesOnly = showFavorites.IsFiltered;
                Settings.Current.ShowPastSessions = showPast.IsFiltered;
                vm.Save ();
                await Navigation.PopModalAsync ();
                if (Device.RuntimePlatform == Device.Android)
                    MessagingService.Current.SendMessage ("filter_changed");

            });

            BindingContext = vm = new FilterSessionsViewModel(Navigation);

            
            
            

         
            LoadCategories ();
        }


        void LoadCategories()
        { 
            vm.LoadCategoriesAsync().ContinueWith((result) =>
                {
                    Device.BeginInvokeOnMainThread(()=>
                        {
                            var allCell = new CategoryCell
                            {
                                BindingContext = vm.AllCategory
                            };

                            TableSectionCategories.Add(allCell);

                            foreach (var item in vm.Categories)
                            {
                                TableSectionCategories.Add(new CategoryCell
                                    {
                                        BindingContext = item
                                    });
                            }

                            var color = Device.RuntimePlatform == Device.UWP ? "#7635EB" : string.Empty;
                             
                            showPast = new Category
                            {
                                Name = "Show Past Sessions",
                                IsEnabled = true,
                                ShortName = "Show Past Sessions",
                                Color = color

                            };

                            showFavorites = new Category
                            {
                                Name = "Show Favorites Only",
                                IsEnabled = true,
                                ShortName = "Show Favorites Only",
                                Color = color

                            };

                            TableSectionFilters.Add(new CategoryCell
                            {
                                BindingContext = showPast
                            });

                            TableSectionFilters.Add(new CategoryCell
                            {
                                BindingContext = showFavorites
                            });
                            
                            //if end of conference
                            if (DateTime.UtcNow > Settings.EndOfConference)
                                showPast.IsEnabled = false;

                            showPast.IsFiltered = Settings.Current.ShowPastSessions;
                            showFavorites.IsFiltered = Settings.Current.FavoritesOnly;


                        });
                });
        }

       
    }
}


using Xamarin.Forms;
using XamarinEvolve.Clients.Portable;
using XamarinEvolve.DataObjects;
using System;
using FormsToolkit;

namespace XamarinEvolve.Clients.UI
{
    public partial class FilterSessionsPage : ContentPage
    {
        FilterSessionsViewModel vm;
        Category showFavorites, showPast;
        public FilterSessionsPage()
        {
            InitializeComponent();

            if (Device.OS != TargetPlatform.iOS)
                ToolbarDone.Icon = "toolbar_close.png";

            ToolbarDone.Command = new Command (async () =>
            {
                Settings.Current.FavoritesOnly = showFavorites.IsFiltered;
                Settings.Current.ShowPastSessions = showPast.IsFiltered;
                vm.Save ();
                await Navigation.PopModalAsync ();
                if (Device.OS == TargetPlatform.Android)
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

                            var color = Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone ? "#7635EB" : string.Empty;
                             
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
                            
                            //if end of evolve
                            if (DateTime.UtcNow > Settings.EndOfEvolve)
                                showPast.IsEnabled = false;

                            showPast.IsFiltered = Settings.Current.ShowPastSessions;
                            showFavorites.IsFiltered = Settings.Current.FavoritesOnly;


                        });
                });
        }

       
    }
}


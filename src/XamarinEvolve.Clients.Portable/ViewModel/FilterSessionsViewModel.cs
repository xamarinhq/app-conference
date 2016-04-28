using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinEvolve.DataObjects;
using FormsToolkit;

namespace XamarinEvolve.Clients.Portable
{
    public class FilterSessionsViewModel : ViewModelBase
    {
        public FilterSessionsViewModel(INavigation navigation)
            : base(navigation)
        {
            
            AllCategory = new Category
                {
                    Name = "All",
                    IsEnabled = true,
                    IsFiltered = Settings.ShowAllCategories
                };

            AllCategory.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "IsFiltered")
                    SetShowAllCategories(AllCategory.IsFiltered);
            };
              
        }


        public Category AllCategory { get; }
        public List<Category> Categories { get; } = new List<Category>();

        private void SetShowAllCategories(bool showAll)
        {
            Settings.ShowAllCategories = showAll;
            foreach(var category in Categories)
            {
                category.IsEnabled = !Settings.ShowAllCategories;
                category.IsFiltered = Settings.ShowAllCategories || Settings.FilteredCategories.Contains(category.Name);
            }
        }

        public async Task LoadCategoriesAsync()
        {
            Categories.Clear();
            var items = await StoreManager.CategoryStore.GetItemsAsync();
            try 
            {
                if (!items.Any ())
                    items = await StoreManager.CategoryStore.GetItemsAsync (true);
            } 
            catch 
            {
                items = await StoreManager.CategoryStore.GetItemsAsync (true);
            }
            
            foreach(var category in items.OrderBy(c => c.Name))
            {
                category.IsFiltered = Settings.ShowAllCategories || Settings.FilteredCategories.Contains(category.Name); 
                category.IsEnabled = !Settings.ShowAllCategories;
                Categories.Add(category);
            }

            Save();
        }

       
        public void Save()
        {
            Settings.FilteredCategories = string.Join("|", Categories?.Where(c => c.IsFiltered).Select(c => c.Name)); 
        }
    }
}


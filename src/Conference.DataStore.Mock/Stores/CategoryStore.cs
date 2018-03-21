using System.Threading.Tasks;
using Conference.DataObjects;
using Conference.DataStore.Abstractions;

using Conference.DataStore.Mock;
using System.Collections.Generic;

namespace Conference.DataStore.Mock
{
    public class CategoryStore : BaseStore<Category>, ICategoryStore
    {
        public override Task<IEnumerable<Category>> GetItemsAsync(bool forceRefresh = false)
        {
           var categories = new []
                {
                    new Category { Name = "Android", ShortName="Android", Color="#B8E986"},
                    new Category { Name = "iOS", ShortName="iOS", Color="#F16EB0"},
                    new Category { Name = "Xamarin.Forms", ShortName="X.Forms", Color="#7DD5C9" },
                    new Category { Name = "Design", ShortName="Design", Color="#51C7E3"},
                    new Category { Name = "Secure", ShortName="Secure", Color="#F88F73" },
                    new Category { Name = "Test", ShortName="Test", Color="#4B637E"},
                    new Category { Name = "Monitor", ShortName="Monitor", Color="#AC9AD3" },
                };
            return Task.FromResult(categories as IEnumerable<Category>);
        }
    }
}


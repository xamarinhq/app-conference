using System;
using System.Threading.Tasks;
using XamarinEvolve.DataObjects;
using XamarinEvolve.DataStore.Abstractions;

using Xamarin.Forms;
using XamarinEvolve.DataStore.Azure;

namespace XamarinEvolve.DataStore.Azure
{
    public class CategoryStore : BaseStore<Category>, ICategoryStore
    {
        public override string Identifier => "Category";
    }
}


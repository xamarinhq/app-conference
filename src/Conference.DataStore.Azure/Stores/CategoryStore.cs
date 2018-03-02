using System;
using System.Threading.Tasks;
using Conference.DataObjects;
using Conference.DataStore.Abstractions;

using Xamarin.Forms;
using Conference.DataStore.Azure;

namespace Conference.DataStore.Azure
{
    public class CategoryStore : BaseStore<Category>, ICategoryStore
    {
        public override string Identifier => "Category";
    }
}


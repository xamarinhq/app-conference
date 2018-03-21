using System;
using Conference.DataObjects;

namespace Conference.DataStore.Abstractions
{
    public interface IEventStore : IBaseStore<FeaturedEvent>
    {
    }
}


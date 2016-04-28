using System;
using XamarinEvolve.DataObjects;
using XamarinEvolve.DataStore.Abstractions;

namespace XamarinEvolve.DataStore.Azure
{
    public class EventStore : BaseStore<FeaturedEvent>, IEventStore
    {
        public override string Identifier => "FeaturedEvent";
        public EventStore()
        {
            
        }
    }
}


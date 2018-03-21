using System;
using Conference.DataObjects;
using Conference.DataStore.Abstractions;

namespace Conference.DataStore.Azure
{
    public class EventStore : BaseStore<FeaturedEvent>, IEventStore
    {
        public override string Identifier => "FeaturedEvent";
        public EventStore()
        {
            
        }
    }
}


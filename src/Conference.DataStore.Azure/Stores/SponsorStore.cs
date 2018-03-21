using Conference.DataStore.Abstractions;
using Conference.DataObjects;
using Conference.DataStore.Azure;

namespace Conference.DataStore.Azure
{
    public class SponsorStore : BaseStore<Sponsor>, ISponsorStore
    {
        public override string Identifier => "Sponsor";
    }
}


using XamarinEvolve.DataStore.Abstractions;
using XamarinEvolve.DataObjects;
using XamarinEvolve.DataStore.Azure;

namespace XamarinEvolve.DataStore.Azure
{
    public class SponsorStore : BaseStore<Sponsor>, ISponsorStore
    {
        public override string Identifier => "Sponsor";
    }
}


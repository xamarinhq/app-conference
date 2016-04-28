using XamarinEvolve.DataStore.Abstractions;
using XamarinEvolve.DataObjects;
using XamarinEvolve.DataStore.Azure;

namespace XamarinEvolve.DataStore.Azure
{
    public class SpeakerStore : BaseStore<Speaker>, ISpeakerStore
    {
        public override string Identifier => "Speaker";
    }
}


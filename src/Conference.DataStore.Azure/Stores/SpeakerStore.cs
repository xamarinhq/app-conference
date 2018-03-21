using Conference.DataStore.Abstractions;
using Conference.DataObjects;
using Conference.DataStore.Azure;

namespace Conference.DataStore.Azure
{
    public class SpeakerStore : BaseStore<Speaker>, ISpeakerStore
    {
        public override string Identifier => "Speaker";
    }
}


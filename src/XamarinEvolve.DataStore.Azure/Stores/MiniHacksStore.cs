using System;
using XamarinEvolve.DataObjects;
using XamarinEvolve.DataStore.Abstractions;

namespace XamarinEvolve.DataStore.Azure
{
    public class MiniHacksStore : BaseStore<MiniHack>, IMiniHacksStore
    {
        public MiniHacksStore()
        {
        }

        public override string Identifier => "MiniHacks";
    }
}


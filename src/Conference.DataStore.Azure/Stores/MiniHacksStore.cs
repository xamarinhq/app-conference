using System;
using Conference.DataObjects;
using Conference.DataStore.Abstractions;

namespace Conference.DataStore.Azure
{
    public class MiniHacksStore : BaseStore<MiniHack>, IMiniHacksStore
    {
        public MiniHacksStore()
        {
        }

        public override string Identifier => "MiniHacks";
    }
}


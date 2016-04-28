using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using XamarinEvolve.DataObjects;

using XamarinEvolve.DataStore.Azure;
using XamarinEvolve.DataStore.Abstractions;

namespace XamarinEvolve.DataStore.Azure
{
    public class FeedbackStore : BaseStore<Feedback>, IFeedbackStore
    {
        public async Task<bool> LeftFeedback(Session session)
        {
            await InitializeStore();
            var items = await Table.Where(s => s.SessionId == session.Id).ToListAsync().ConfigureAwait (false);
            return items.Count > 0;
        }

        public Task DropFeedback()
        {
            return Task.FromResult(true);
        }

     

        public override string Identifier => "Feedback";
         
    }
}


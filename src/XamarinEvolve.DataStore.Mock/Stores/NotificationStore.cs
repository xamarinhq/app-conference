using System;
using XamarinEvolve.DataObjects;
using XamarinEvolve.DataStore.Abstractions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace XamarinEvolve.DataStore.Mock
{
    public class NotificationStore  : BaseStore<Notification>, INotificationStore
    {
        public NotificationStore()
        {
        }

        public async Task<Notification> GetLatestNotification()
        {
            var items = await GetItemsAsync();
            return items.ElementAt(0);
        }

        public override Task<IEnumerable<Notification>> GetItemsAsync(bool forceRefresh = false)
        {
            var items = new []
            {
                new Notification
                {
                    Date = DateTime.UtcNow,
                    Text = "Welcome to Xamarin Evolve!"
                }
            };
            return Task.FromResult(items as IEnumerable<Notification>);
        }
    }
}


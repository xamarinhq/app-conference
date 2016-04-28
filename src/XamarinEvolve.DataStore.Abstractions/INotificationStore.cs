using System;
using XamarinEvolve.DataObjects;
using System.Threading.Tasks;

namespace XamarinEvolve.DataStore.Abstractions
{
    public interface INotificationStore : IBaseStore<Notification>
    {
        Task<Notification> GetLatestNotification();
    }
}


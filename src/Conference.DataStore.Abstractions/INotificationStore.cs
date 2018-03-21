using System;
using Conference.DataObjects;
using System.Threading.Tasks;

namespace Conference.DataStore.Abstractions
{
    public interface INotificationStore : IBaseStore<Notification>
    {
        Task<Notification> GetLatestNotification();
    }
}


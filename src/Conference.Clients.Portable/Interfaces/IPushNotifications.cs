using System;
using System.Threading.Tasks;

namespace Conference.Clients.Portable
{
    public interface IPushNotifications
    {
        Task<bool> RegisterForNotifications();

        bool IsRegistered { get; }

        void OpenSettings();
    }
}


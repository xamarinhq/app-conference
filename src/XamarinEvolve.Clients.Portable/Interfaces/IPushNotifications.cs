using System;
using System.Threading.Tasks;

namespace XamarinEvolve.Clients.Portable
{
    public interface IPushNotifications
    {
        Task<bool> RegisterForNotifications();

        bool IsRegistered { get; }

        void OpenSettings();
    }
}


using System;

namespace Conference.Clients.Portable
{
    public interface IToast
    {
        void SendToast(string message);
    }
}


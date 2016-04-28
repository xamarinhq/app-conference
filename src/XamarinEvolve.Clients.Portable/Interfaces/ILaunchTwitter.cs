using System;

namespace XamarinEvolve.Clients.Portable
{
    public interface ILaunchTwitter
    {
        bool OpenUserName(string username);
        bool OpenStatus(string statusId);
    }
}


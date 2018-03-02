using System;
using System.Threading.Tasks;

namespace Conference.Clients.Portable
{
    public interface IWiFiConfig
    {
        bool ConfigureWiFi(string ssid, string password);
        bool IsConfigured(string ssid);
        bool IsWiFiOn();
    }
}


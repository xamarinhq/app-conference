using System;
using XamarinEvolve.Clients.Portable;
using System.Threading.Tasks;
using System.Collections.Generic;
using Android.Net.Wifi;
using Xamarin.Forms;
using Android.Content;
using System.Diagnostics;
using System.Linq;
using XamarinEvolve.Droid;

[assembly:Dependency(typeof(WiFiConfig))]
namespace XamarinEvolve.Droid
{
    public class WiFiConfig : IWiFiConfig
    {
        
        #region IWiFiConfig implementation

        public bool ConfigureWiFi(string ssid, string password)
        {
            try
            {
                var wifiConfig = GetWifiConfig(ssid, password);
                var wifiManager = Forms.Context.GetSystemService(Context.WifiService) as WifiManager;

                if(wifiManager == null)
                    return false;

                var netId = wifiManager.AddNetwork(wifiConfig);
                if(netId != -1)
                {
                    wifiManager.EnableNetwork(netId, false);
                    var result = wifiManager.SaveConfiguration();
                    if(!result)
                    {
                        Debug.WriteLine("Unknown error while calling WiFiManager.SaveConfiguration();");
                        return false;
                    }
                }
                else
                {
                    Debug.WriteLine("Unknown error while calling WiFiManager.AddNetwork();");
                    return false;
                }
            }
            catch(Exception ex)
            {
                DependencyService.Get<ILogger>()?.Report(ex);
                return false;
            }

            return true;
        }

        public bool IsConfigured(string ssid)
        {
            var wifiManager = Forms.Context.GetSystemService(Context.WifiService) as WifiManager;
            if(wifiManager == null)
                return false;

            var finalSsid  = string.Format("\"{0}\"", ssid);
            foreach (var id in wifiManager.ConfiguredNetworks)
            {
                if (id == null)
                    continue;


                if (string.IsNullOrWhiteSpace (id.Ssid))
                    continue;
                
                if (id.Ssid.Equals (finalSsid, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }

            return false;
        }

        public bool IsWiFiOn()
        {
            var wifiManager = Forms.Context.GetSystemService(Context.WifiService) as WifiManager;

            return wifiManager?.IsWifiEnabled ?? false;
        }

        #endregion

        // Must be in double quotes to tell system this is an ASCII SSID and passphrase.
        private WifiConfiguration GetWifiConfig (string ssid, string password)
        {

            if(string.IsNullOrWhiteSpace(password))
                return new WifiConfiguration
                {
                    Ssid = Java.Lang.String.Format ("\"%s\"", ssid)
                };

            return new WifiConfiguration
            {
                Ssid = Java.Lang.String.Format ("\"%s\"", ssid),
                PreSharedKey = Java.Lang.String.Format ("\"%s\"", password)
            };
        }
    }
}


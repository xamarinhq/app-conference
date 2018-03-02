using System;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using FormsToolkit;
using Plugin.Share;
using System.Net.Http;
using Plugin.Connectivity;
using Newtonsoft.Json;

namespace Conference.Clients.Portable
{
    public class WiFiRoot
    {
        public string SSID { get; set; }
        public string Password { get; set; }
    }

    public class ConferenceInfoViewModel : ViewModelBase
    {

        IWiFiConfig wiFiConfig;
        const string WifiUrl =  "https://s3.amazonaws.com/xamarin-releases/conference-2016/wifi.json";
        public ConferenceInfoViewModel()
        {
            wiFiConfig = DependencyService.Get<IWiFiConfig>();
        }

        public async Task<bool> UpdateConfigs()
        {
            if (IsBusy)
                return false;


            try 
            {
                IsBusy = true;
                try 
                {
                    if (CrossConnectivity.Current.IsConnected) 
                    {
                        using (var client = new HttpClient ()) 
                        {
                            client.Timeout = TimeSpan.FromSeconds (5);
                            var json = await client.GetStringAsync (WifiUrl);
                            var root = JsonConvert.DeserializeObject<WiFiRoot> (json);
                            Settings.WiFiSSID = root.SSID;
                            Settings.WiFiPass = root.Password;
                        }
                    }
                } 
                catch 
                {
                }

                try 
                {

                    if (wiFiConfig != null)
                        WiFiConfigured = wiFiConfig.IsConfigured (Settings.WiFiSSID);
                } 
                catch (Exception ex) 
                {
                    ex.Data ["Method"] = "UpdateConfigs";
                    Logger.Report (ex);
                    return false;
                }

            } 
            finally 
            {
                IsBusy = false;
            }

            return true;
        }


        bool wiFiConfigured;
        public bool WiFiConfigured
        {
            get { return wiFiConfigured; }
            set { SetProperty(ref wiFiConfigured, value); }
        }


        ICommand  configureWiFiCommand;
        public ICommand ConfigureWiFiCommand =>
            configureWiFiCommand ?? (configureWiFiCommand = new Command(ExecuteConfigureWiFiCommand)); 

        void ExecuteConfigureWiFiCommand()
        {
            if(wiFiConfig == null)
                return;

            Logger.Track(ConferenceLoggerKeys.WiFiConfig, "Type", "2.4Ghz");

            if (!wiFiConfig.ConfigureWiFi(Settings.WiFiSSID, Settings.WiFiPass))
            {
                WiFiConfigured = false;
                SendWiFiError();
            }
            else
            {
                WiFiConfigured = true;
            }
        }

        void SendWiFiError()
        {
            MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message,
                new MessagingServiceAlert
                {
                    Title="Wi-Fi Configuration",
                    Message ="Unable to configure WiFi, you may have to configure manually or try again.",
                    Cancel = "OK"
                });
        }

        ICommand  copyPasswordCommand;
        public ICommand CopyPasswordCommand =>
        copyPasswordCommand ?? (copyPasswordCommand = new Command<string>(async (t) => await ExecuteCopyPasswordAsync(t))); 

        async Task ExecuteCopyPasswordAsync(string pass)
        {
            Logger.Track(ConferenceLoggerKeys.CopyPassword);
            await CrossShare.Current.SetClipboardText(pass, "Password");
            Toast.SendToast("Password Copied");
        }
    }
}


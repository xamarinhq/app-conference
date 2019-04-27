using FormsToolkit;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

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

                    Settings.WiFiSSID ="Xamarin_Evolve";
                    Settings.WiFiPass = string.Empty;
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
            get => wiFiConfigured;
            set => SetProperty(ref wiFiConfigured, value);
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
            await Clipboard.SetTextAsync(pass);
            Toast.SendToast("Password Copied");
        }
    }
}


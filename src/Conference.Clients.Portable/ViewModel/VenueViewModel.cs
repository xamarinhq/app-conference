using FormsToolkit;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Conference.Clients.Portable
{
    public class VenueViewModel : ViewModelBase
    {
        public bool CanMakePhoneCall => true;
        public string EventTitle => "Conference";
        public string LocationTitle => "Hyatt Regency Orlando";
        public string Address1 => "9801 International Drive";
        public string Address2 => "Orlando, FL 32819";
        public double Latitude => 28.427015;
        public double Longitude => -81.467563;

        ICommand  navigateCommand;
        public ICommand NavigateCommand =>
            navigateCommand ?? (navigateCommand = new Command(async () => await ExecuteNavigateCommandAsync())); 

        async Task ExecuteNavigateCommandAsync()
        {
            Logger.Track(ConferenceLoggerKeys.NavigateToConference);
            await Map.OpenAsync(Latitude, Longitude, new MapLaunchOptions
            {
                Name = LocationTitle,
                NavigationMode = NavigationMode.Default
            });
        }

        ICommand  callCommand;
        public ICommand CallCommand =>
            callCommand ?? (callCommand = new Command(ExecuteCallCommand)); 

        void ExecuteCallCommand()
        {
            Logger.Track(ConferenceLoggerKeys.CallHotel);
            try
            {
                PhoneDialer.Open("14072841234");
            }
            catch (FeatureNotSupportedException)
            {
                Application.Current?.MainPage?.DisplayAlert("Sorry!", "Your device doesn't appear to support phone calls!", "OK");
            }
        }
    }
}



using System;

using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using Plugin.ExternalMaps;
using Plugin.Messaging;
using FormsToolkit;

namespace XamarinEvolve.Clients.Portable
{
    public class VenueViewModel : ViewModelBase
    {
        public bool CanMakePhoneCall => CrossMessaging.Current.PhoneDialer.CanMakePhoneCall;
        public string EventTitle => "Xamarin Evolve";
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
            Logger.Track(EvolveLoggerKeys.NavigateToEvolve);
            if(!await CrossExternalMaps.Current.NavigateTo(LocationTitle, Latitude, Longitude))
            {
                MessagingService.Current.SendMessage(MessageKeys.Message, new MessagingServiceAlert
                    {
                        Title = "Unable to Navigate",
                        Message = "Please ensure that you have a map application installed.",
                        Cancel = "OK"
                    });
            }
        }

        ICommand  callCommand;
        public ICommand CallCommand =>
            callCommand ?? (callCommand = new Command(ExecuteCallCommand)); 

        void ExecuteCallCommand()
        {
            Logger.Track(EvolveLoggerKeys.CallHotel);
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall) 
                phoneCallTask.MakePhoneCall("14072841234");
        }
    }
}



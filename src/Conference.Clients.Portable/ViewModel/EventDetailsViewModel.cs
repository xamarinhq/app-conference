using System;

using Xamarin.Forms;
using Conference.DataObjects;
using System.Windows.Input;
using MvvmHelpers;
using FormsToolkit;
using System.Threading.Tasks;

using Xamarin.Essentials;

namespace Conference.Clients.Portable
{
    public class EventDetailsViewModel : ViewModelBase
    {
        public FeaturedEvent Event { get; set; }

        public ObservableRangeCollection<Sponsor> Sponsors { get; set; }

        public EventDetailsViewModel(INavigation navigation, FeaturedEvent e) : base(navigation)
        {
            Event = e;
            Sponsors = new ObservableRangeCollection<Sponsor>();
            if (e.Sponsor != null)
                Sponsors.Add(e.Sponsor);
        }

        bool isReminderSet;
        public bool IsReminderSet
        {
            get => isReminderSet;
            set => SetProperty(ref isReminderSet, value);
        }

        ICommand  loadEventDetailsCommand;
        public ICommand LoadEventDetailsCommand =>
            loadEventDetailsCommand ?? (loadEventDetailsCommand = new Command(async () => await ExecuteLoadEventDetailsCommandAsync())); 

        async Task ExecuteLoadEventDetailsCommandAsync()
        {

            if(IsBusy)
                return;

            try 
            {


                IsBusy = true;
                IsReminderSet = await ReminderService.HasReminderAsync("event_" + Event.Id);
            } 
            catch (Exception ex) 
            {
                Logger.Report(ex, "Method", "ExecuteLoadEventDetailsCommandAsync");
                MessagingService.Current.SendMessage(MessageKeys.Error, ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        ICommand  reminderCommand;
        public ICommand ReminderCommand =>
            reminderCommand ?? (reminderCommand = new Command(async () => await ExecuteReminderCommandAsync())); 


        async Task ExecuteReminderCommandAsync()
        {
            if(!IsReminderSet)
            {
                var result = await ReminderService.AddReminderAsync("event_" + Event.Id, 
                    new Plugin.Calendars.Abstractions.CalendarEvent
                    {
                        Description = Event.Description,
                        Location = Event.LocationName,
                        AllDay = Event.IsAllDay,
                        Name = Event.Title,
                        Start = Event.StartTime.Value,
                        End = Event.EndTime.Value
                    });


                if(!result)
                    return;

                Logger.Track(ConferenceLoggerKeys.ReminderAdded, "Title", Event.Title);
                IsReminderSet = true;
            }
            else
            {
                var result = await ReminderService.RemoveReminderAsync("event_" + Event.Id);
                if(!result)
                    return;
                Logger.Track(ConferenceLoggerKeys.ReminderRemoved, "Title", Event.Title);
                IsReminderSet = false;
            }

        }

        Sponsor selectedSponsor;
        public Sponsor SelectedSponsor
        {
            get => selectedSponsor;
            set
            {
                selectedSponsor = value;
                OnPropertyChanged();
                if (selectedSponsor == null)
                    return;

                MessagingService.Current.SendMessage(MessageKeys.NavigateToSponsor, selectedSponsor);

                SelectedSponsor = null;
            }
        }

    }
}



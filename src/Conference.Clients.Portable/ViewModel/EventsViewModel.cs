using System;
using Xamarin.Forms;
using Conference.DataObjects;
using MvvmHelpers;
using FormsToolkit;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Linq;

namespace Conference.Clients.Portable
{
    public class EventsViewModel : ViewModelBase
    {
        public EventsViewModel(INavigation navigation) : base(navigation)
        {
            Title = "Events";
        }


        public ObservableRangeCollection<FeaturedEvent> Events { get; } = new ObservableRangeCollection<FeaturedEvent>();
        public ObservableRangeCollection<Grouping<string, FeaturedEvent>> EventsGrouped { get; } = new ObservableRangeCollection<Grouping<string, FeaturedEvent>>();



        #region Properties
        FeaturedEvent selectedEvent;
        public FeaturedEvent SelectedEvent
        {
            get => selectedEvent;
            set
            {
                selectedEvent = value;
                OnPropertyChanged();
                if (selectedEvent == null)
                    return;

                MessagingService.Current.SendMessage(MessageKeys.NavigateToEvent, selectedEvent);

                SelectedEvent = null;
            }
        }


        #endregion

        #region Sorting


        void SortEvents()
        {
            EventsGrouped.ReplaceRange(Events.GroupByDate());
        }


        #endregion


        #region Commands

        ICommand  forceRefreshCommand;
        public ICommand ForceRefreshCommand =>
        forceRefreshCommand ?? (forceRefreshCommand = new Command(async () => await ExecuteForceRefreshCommandAsync())); 

        async Task ExecuteForceRefreshCommandAsync()
        {
            await ExecuteLoadEventsAsync(true);
        }

        ICommand loadEventsCommand;
        public ICommand LoadEventsCommand =>
            loadEventsCommand ?? (loadEventsCommand = new Command<bool>(async (f) => await ExecuteLoadEventsAsync())); 

        async Task<bool> ExecuteLoadEventsAsync(bool force = false)
        {
            if(IsBusy)
                return false;

            try 
            {
                IsBusy = true;

                #if DEBUG
                await Task.Delay(1000);
                #endif

                Events.ReplaceRange(await StoreManager.EventStore.GetItemsAsync(force));

                Title = "Events (" + Events.Count(e => e.StartTime.HasValue && e.StartTime.Value > DateTime.UtcNow) + ")";

                SortEvents();

            } 
            catch (Exception ex) 
            {
                Logger.Report(ex, "Method", "ExecuteLoadEventsAsync");
                MessagingService.Current.SendMessage(MessageKeys.Error, ex);
            }
            finally
            {
                IsBusy = false;
            }

            return true;
        }
        #endregion
    }
}


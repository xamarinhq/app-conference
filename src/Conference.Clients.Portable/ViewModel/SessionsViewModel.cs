using System;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using MvvmHelpers;
using FormsToolkit;
using Conference.DataObjects;
using Xamarin.Essentials;

namespace Conference.Clients.Portable
{
    public class SessionsViewModel : ViewModelBase
    {
        public SessionsViewModel(INavigation navigation) : base(navigation)
        {
            NextForceRefresh = DateTime.UtcNow.AddMinutes(45);
        }


        public ObservableRangeCollection<Session> Sessions { get; } = new ObservableRangeCollection<Session>();
        public ObservableRangeCollection<Session> SessionsFiltered { get; } = new ObservableRangeCollection<Session>();
        public ObservableRangeCollection<Grouping<string, Session>> SessionsGrouped { get; } = new ObservableRangeCollection<Grouping<string, Session>>();
        public DateTime NextForceRefresh { get; set; }


        #region Properties
        Session selectedSession;
        public Session SelectedSession
        {
            get => selectedSession;
            set
            {
                selectedSession = value;
                OnPropertyChanged();
                if (selectedSession == null)
                    return;

                MessagingService.Current.SendMessage(MessageKeys.NavigateToSession, selectedSession);

                SelectedSession = null;
            }
        }

        string filter = string.Empty;
        public string Filter
        {
            get => filter;
            set
            {
                if (SetProperty(ref filter, value))
                    ExecuteFilterSessionsAsync();

            }
        }
        #endregion

        #region Filtering and Sorting


        void SortSessions()
        {
            SessionsGrouped.ReplaceRange(SessionsFiltered.FilterAndGroupByDate());


        }


        bool noSessionsFound;
        public bool NoSessionsFound
        {
            get => noSessionsFound;
            set => SetProperty(ref noSessionsFound, value);
        }

        string noSessionsFoundMessage;
        public string NoSessionsFoundMessage
        {
            get => noSessionsFoundMessage;
            set => SetProperty(ref noSessionsFoundMessage, value);
        }

        #endregion


        #region Commands

        ICommand  forceRefreshCommand;
        public ICommand ForceRefreshCommand =>
        forceRefreshCommand ?? (forceRefreshCommand = new Command(async () => await ExecuteForceRefreshCommandAsync())); 

        async Task ExecuteForceRefreshCommandAsync()
        {
            await ExecuteLoadSessionsAsync(true);
        }

        ICommand  filterSessionsCommand;
        public ICommand FilterSessionsCommand =>
            filterSessionsCommand ?? (filterSessionsCommand = new Command(async () => await ExecuteFilterSessionsAsync())); 

        async Task ExecuteFilterSessionsAsync()
        {
            IsBusy = true;
            NoSessionsFound = false;

            // Abort the current command if the query has changed and is not empty
            if (!string.IsNullOrEmpty(Filter))
            {
                var query = Filter;
                await Task.Delay(250);
                if (query != Filter) 
                    return;
            }

            SessionsFiltered.ReplaceRange(Sessions.Search(Filter));
            SortSessions();

            if(SessionsGrouped.Count == 0)
            {
                if(Settings.Current.FavoritesOnly)
                {
                    if(!Settings.Current.ShowPastSessions && !string.IsNullOrWhiteSpace(Filter))
                        NoSessionsFoundMessage = "You haven't favorited\nany sessions yet.";
                    else
                        NoSessionsFoundMessage = "No Sessions Found";
                }
                else
                    NoSessionsFoundMessage = "No Sessions Found";

                NoSessionsFound = true;
            }
            else
            {
                NoSessionsFound = false;
            }

            IsBusy = false;
        }



        ICommand loadSessionsCommand;
        public ICommand LoadSessionsCommand =>
            loadSessionsCommand ?? (loadSessionsCommand = new Command<bool>(async (f) => await ExecuteLoadSessionsAsync())); 


        async Task<bool> ExecuteLoadSessionsAsync(bool force = false)
        {
            if(IsBusy)
                return false;

            try 
            {
                NextForceRefresh = DateTime.UtcNow.AddMinutes(45);
                IsBusy = true;
                NoSessionsFound = false;
                Filter = string.Empty;

                #if DEBUG
                await Task.Delay(1000);
                #endif

                Sessions.ReplaceRange(await StoreManager.SessionStore.GetItemsAsync(force));

                SessionsFiltered.ReplaceRange(Sessions);
                SortSessions();

                if(SessionsGrouped.Count == 0)
                {
                    
                    if(Settings.Current.FavoritesOnly)
                    {
                        if(!Settings.Current.ShowPastSessions)
                            NoSessionsFoundMessage = "You haven't favorited\nany sessions yet.";
                        else
                            NoSessionsFoundMessage = "No Sessions Found";
                    }
                    else
                        NoSessionsFoundMessage = "No Sessions Found";

                    NoSessionsFound = true;
                }
                else
                {
                    NoSessionsFound = false;
                }
                
                

            } 
            catch (Exception ex) 
            {
                Logger.Report(ex, "Method", "ExecuteLoadSessionsAsync");
                MessagingService.Current.SendMessage(MessageKeys.Error, ex);
            }
            finally
            {
                IsBusy = false;
            }

            return true;
        }

        ICommand  favoriteCommand;
        public ICommand FavoriteCommand =>
            favoriteCommand ?? (favoriteCommand = new Command<Session>(async (s) => await ExecuteFavoriteCommandAsync(s))); 

        async Task ExecuteFavoriteCommandAsync(Session session)
        {
            var toggled = await FavoriteService.ToggleFavorite(session);
            if(toggled && Settings.Current.FavoritesOnly)
                SortSessions();
        }


        #endregion
    }
}


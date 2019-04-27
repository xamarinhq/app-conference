using System;
using System.Collections.Generic;
using System.Windows.Input;
using FormsToolkit;
using MvvmHelpers;
using Xamarin.Forms;
using Conference.DataObjects;
using System.Linq;
using System.Threading.Tasks;

namespace Conference.Clients.Portable
{
    public class EvaluationsViewModel: ViewModelBase
    {
        public EvaluationsViewModel (INavigation navigation) : base(navigation)
        {
            NextForceRefresh = DateTime.UtcNow.AddMinutes (45);
        }

        bool sync = true;

        public static bool ForceRefresh { get; set; }

        public ObservableRangeCollection<Session> Sessions { get; } = new ObservableRangeCollection<Session> ();
        public DateTime NextForceRefresh { get; set; }


        bool noSessionsFound;
        public bool NoSessionsFound {
            get => noSessionsFound;
            set => SetProperty(ref noSessionsFound, value);
        }

        string noSessionsFoundMessage;
        public string NoSessionsFoundMessage {
            get => noSessionsFoundMessage;
            set => SetProperty(ref noSessionsFoundMessage, value);
        }


        ICommand loadSessionsCommand;
        public ICommand LoadSessionsCommand =>
            loadSessionsCommand ?? (loadSessionsCommand = new Command (async () => await ExecuteLoadSessionsAsync ()));


        async Task<bool> ExecuteLoadSessionsAsync ()
        {
            if (IsBusy)
                return false;

            try 
            {
                NextForceRefresh = DateTime.UtcNow.AddMinutes (45);
                IsBusy = true;
                NoSessionsFound = false;

#if DEBUG
                await Task.Delay(1000);

#endif

                if (!Settings.IsLoggedIn) 
                {
                    NoSessionsFoundMessage = "Please sign in\nto leave feedback";
                    NoSessionsFound = true;
                    return true;
                }

                var sessions = (await StoreManager.SessionStore.GetItemsAsync ()).ToList();
                var feedback = (await StoreManager.FeedbackStore.GetItemsAsync (sync)).ToList();

                sync = false;

                var finalSessions = new List<Session> ();
                foreach (var session in sessions) 
                {
                    if (!(await StoreManager.FavoriteStore.IsFavorite (session.Id)))
                        continue;
                    
                    //if TBA
                    if (!session.StartTime.HasValue)
                        continue;
#if !DEBUG

                    //if it hasn't started yet
                    if (DateTime.UtcNow < session.StartTime.Value)
                        continue;
#endif
                    if (feedback.Any (f => f.SessionId == session.Id))
                        continue;

                    finalSessions.Add (session);
                }

                Sessions.ReplaceRange (finalSessions);


                if (Sessions.Count == 0) 
                {
                    NoSessionsFoundMessage = "No Pending\nEvaluations Found";
                    NoSessionsFound = true;
                } 
                else 
                {
                    NoSessionsFound = false;
                }
            } 
            catch (Exception ex) 
            {
                Logger.Report (ex, "Method", "ExecuteLoadSessionsAsync");
                MessagingService.Current.SendMessage (MessageKeys.Error, ex);
            } 
            finally 
            {
                IsBusy = false;
            }

            return true;
        }
    }
}


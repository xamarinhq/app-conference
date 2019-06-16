using System;
using System.Windows.Input;
using System.Threading.Tasks;
using MvvmHelpers;
using System.Linq;
using Xamarin.Forms;
using FormsToolkit;
using System.Reflection;
using Newtonsoft.Json;
using Conference.DataObjects;
using System.Net.Http;
using System.Collections.Generic;
using Conference.DataStore.Abstractions;
using Conference.Utils.Helpers;
using Xamarin.Essentials;

namespace Conference.Clients.Portable
{
    public class FeedViewModel : ViewModelBase
    {

        public ObservableRangeCollection<Tweet> Tweets { get; } = new ObservableRangeCollection<Tweet>();
        public ObservableRangeCollection<Session> Sessions { get; } = new ObservableRangeCollection<Session>();
        public DateTime NextForceRefresh { get; set; }
        public FeedViewModel()
        {
            NextForceRefresh = DateTime.UtcNow.AddMinutes(45);
        }


        ICommand  refreshCommand;
        public ICommand RefreshCommand =>
            refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommandAsync())); 

        async Task ExecuteRefreshCommandAsync()
        {
            try
            {
                NextForceRefresh = DateTime.UtcNow.AddMinutes(45);
                IsBusy = true;
                var tasks = new Task[]
                    {
                        ExecuteLoadNotificationsCommandAsync(),
                        ExecuteLoadSocialCommandAsync(),
                        ExecuteLoadSessionsCommandAsync()
                    };

                await Task.WhenAll(tasks);
            }
            catch(Exception ex)
            {
                ex.Data["method"] = "ExecuteRefreshCommandAsync";
                Logger.Report(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }



        ICommand shareCommand;
        public ICommand ShareCommand =>
            shareCommand ?? (shareCommand = new Command(async () => await ExecuteShareCommandAsync()));

        async Task ExecuteShareCommandAsync()
        {
            Logger.Track(ConferenceLoggerKeys.Share, "Title", "Noticias");

            await Share.RequestAsync(new ShareTextRequest
            {
                Text = $"Únete a nuestra maravillosa comunidad #LaComarca. Te esperamos ! " +
                $"{Environment.NewLine + Environment.NewLine} Link: https://t.me/lacomarcaDO",
                Title = "Share"
            });
        }

        Notification notification;
        public Notification Notification
        {
            get => notification;
            set => SetProperty(ref notification, value);
        }

        bool loadingNotifications;
        public bool LoadingNotifications
        {
            get => loadingNotifications;
            set => SetProperty(ref loadingNotifications, value);
        }

        ICommand  loadNotificationsCommand;
        public ICommand LoadNotificationsCommand =>
            loadNotificationsCommand ?? (loadNotificationsCommand = new Command(async () => await ExecuteLoadNotificationsCommandAsync())); 

        async Task ExecuteLoadNotificationsCommandAsync()
        {
            if (LoadingNotifications)
                return;
            LoadingNotifications = true;
            #if DEBUG
            await Task.Delay(1000);
            #endif

            try
            {
                Notification = await StoreManager.NotificationStore.GetLatestNotification();
            }
            catch(Exception ex)
            {
                ex.Data["method"] = "ExecuteLoadNotificationsCommandAsync";
                Logger.Report(ex);
                Notification = new Notification
                    {
                        Date = DateTime.UtcNow,
                        Text = "Welcome to Conference!"
                    };   
            }
            finally
            {
                LoadingNotifications = false;
            }
        }


        #region Sessions
        bool loadingSessions;
        public bool LoadingSessions
        {
            get => loadingSessions;
            set => SetProperty(ref loadingSessions, value);
        }

        ICommand loadSessionsCommand;
        public ICommand LoadSessionsCommand =>
            loadSessionsCommand ?? (loadSessionsCommand = new Command(async () => await ExecuteLoadSessionsCommandAsync()));

        async Task ExecuteLoadSessionsCommandAsync()
        {
            if (LoadingSessions)
                return;

            LoadingSessions = true;

            try
            {
                NoSessions = false;
                Sessions.Clear();
                OnPropertyChanged("Sessions");
#if DEBUG
                await Task.Delay(1000);
#endif
                var sessions = await StoreManager.SessionStore.GetNextSessions();
                if (sessions != null)
                    Sessions.AddRange(sessions);

                NoSessions = Sessions.Count == 0;
            }
            catch (Exception ex)
            {
                ex.Data["method"] = "ExecuteLoadSessionsCommandAsync";
                Logger.Report(ex);
                NoSessions = true;
            }
            finally
            {
                LoadingSessions = false;
            }

        }

        bool noSessions;
        public bool NoSessions
        {
            get => noSessions;
            set => SetProperty(ref noSessions, value);
        }

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
        #endregion


        #region Social
        bool loadingSocial;
        public bool LoadingSocial
        {
            get => loadingSocial;
            set => SetProperty(ref loadingSocial, value);
        }

        //public bool ShowBuyTicketButton => FeatureFlags.ShowBuyTicketButton && Utils.EventInfo.StartOfConference.AddDays(-1) >= DateTime.Now;

        ICommand loadSocialCommand;
        public ICommand LoadSocialCommand =>
            loadSocialCommand ?? (loadSocialCommand = new Command(async () => await ExecuteLoadSocialCommandAsync()));

        async Task ExecuteLoadSocialCommandAsync()
        {
            if (LoadingSocial)
                return;

            LoadingSocial = true;
            try
            {
                SocialError = false;
                Tweets.Clear();

                using (var client = new HttpClient())
                {
#if DEBUG
                    Tweets.ReplaceRange(new List<Tweet>
                    {
                        new Tweet
                        {
                            CreatedDate = DateTime.Now,
                            Name = "James Montemagno",
                            ScreenName = "@JamesMontemagno",
                            Image = "https://pbs.twimg.com/profile_images/852007857729847296/0_c-XWrD_200x200.jpg",
                            Text = "Xamarin is amazing for cross-platform mobile development in C#!",
                            TweetedImage = "https://pbs.twimg.com/media/Dx3nhI9U8AAbedK?format=jpg&name=small",
                            Url = "https://twitter.com/JamesMontemagno/status/1091428914847612928"
                        }
                    });
#else


                    var manager = DependencyService.Get<IStoreManager>() as Conference.DataStore.Azure.StoreManager;
                    if (manager == null)
                        return;

                    await manager.InitializeAsync ();

                    var mobileClient = DataStore.Azure.StoreManager.MobileService;
                    if (mobileClient == null)
                        return;
                    
                    var json =  await mobileClient.InvokeApiAsync<string> ("Tweet", System.Net.Http.HttpMethod.Get, null);

                    if (string.IsNullOrWhiteSpace(json)) 
                    {
                        SocialError = true;
                        return;
                    }


                    Tweets.ReplaceRange(JsonConvert.DeserializeObject<List<Tweet>>(json));
#endif
                }

            }
            catch (Exception ex)
            {
                SocialError = true;
                ex.Data["method"] = "ExecuteLoadSocialCommandAsync";
                Logger.Report(ex);
            }
            finally
            {
                LoadingSocial = false;
            }

        }

        bool socialError;
        public bool SocialError
        {
            get => socialError;
            set => SetProperty(ref socialError, value);
        }

        Tweet selectedTweet;
        public Tweet SelectedTweet
        {
            get => selectedTweet;
            set
            {
                selectedTweet = value;
                OnPropertyChanged();
                if (selectedTweet == null)
                    return;

                LaunchBrowserCommand.Execute(selectedTweet.Url);

                SelectedTweet = null;
            }
        } 
        #endregion

        #region Favorite
        ICommand favoriteCommand;
        public ICommand FavoriteCommand =>
        favoriteCommand ?? (favoriteCommand = new Command<Session>((s) => ExecuteFavoriteCommand(s)));

        void ExecuteFavoriteCommand(Session session)
        {
            MessagingService.Current.SendMessage<MessagingServiceQuestion>(MessageKeys.Question, new MessagingServiceQuestion
            {
                Negative = "Cancel",
                Positive = "Unfavorite",
                Question = "Are you sure you want to remove this session from your favorites?",
                Title = "Unfavorite Session",
                OnCompleted = (async (result) =>
                    {
                        if (!result)
                            return;

                        var toggled = await FavoriteService.ToggleFavorite(session);
                        if (toggled)
                            await ExecuteLoadSessionsCommandAsync();
                    })
            });

        } 
        #endregion
    }
}


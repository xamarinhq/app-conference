using System;
using Xamarin.Forms;
using Conference.DataObjects;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using MvvmHelpers;
using FormsToolkit;

namespace Conference.Clients.Portable
{
    public class SpeakerDetailsViewModel : ViewModelBase
    {
        
        public Speaker Speaker { get; set;}
        public ObservableRangeCollection<Session> Sessions { get; } = new ObservableRangeCollection<Session>();
        public ObservableRangeCollection<MenuItem> FollowItems { get; } = new ObservableRangeCollection<MenuItem>();

        bool hasAdditionalSessions;
        public bool HasAdditionalSessions
        {
            get { return hasAdditionalSessions; }
            set { SetProperty(ref hasAdditionalSessions, value); }
        }

        private string sessionId;
        public SpeakerDetailsViewModel(Speaker speaker, string sessionId) : base()
        {
            Speaker = speaker;
            this.sessionId = sessionId;
            if (!string.IsNullOrWhiteSpace(speaker.CompanyWebsiteUrl))
            {
                FollowItems.Add(new MenuItem
                    {
                        Name = "Web",
                        Subtitle = speaker.CompanyWebsiteUrl,
                        Parameter = speaker.CompanyWebsiteUrl,
                        Icon = "icon_website.png"
                    });
            }

            if (!string.IsNullOrWhiteSpace(speaker.BlogUrl))
            {
                FollowItems.Add(new MenuItem
                    {
                        Name = "Blog",
                        Subtitle = speaker.BlogUrl,
                        Parameter = speaker.BlogUrl,
                        Icon = "icon_blog.png"
                    });
            }

            if (!string.IsNullOrWhiteSpace(speaker.TwitterUrl))
            {
                FollowItems.Add(new MenuItem
                    {
                        Name = Device.RuntimePlatform == Device.iOS ? "Twitter" : speaker.TwitterUrl,
                        Subtitle = $"@{speaker.TwitterUrl}",
                        Parameter = "http://twitter.com/" + speaker.TwitterUrl,
                        Icon = "icon_twitter.png"
                    });
            }

            if (!string.IsNullOrWhiteSpace(speaker.LinkedInUrl))
            {
                FollowItems.Add(new MenuItem
                    {
                        Name = "LinkedIn",
                        Subtitle = speaker.LinkedInUrl,
                        Parameter = "http://linkedin.com/in/" + speaker.LinkedInUrl,
                        Icon = "icon_linkedin.png"
                    });
            }

        }

        ICommand  loadSessionsCommand;
        public ICommand LoadSessionsCommand =>
            loadSessionsCommand ?? (loadSessionsCommand = new Command(async () => await ExecuteLoadSessionsCommandAsync())); 

        public async Task ExecuteLoadSessionsCommandAsync()
        {
            if(IsBusy)
                return;
            
            try
            {
                IsBusy = true;

                #if DEBUG
                await Task.Delay(1000);
                #endif


                var items = (await StoreManager.SessionStore.GetSpeakerSessionsAsync(Speaker.Id)).Where(x => x.Id != sessionId);

                Sessions.ReplaceRange(items);

                HasAdditionalSessions = Sessions.Count > 0;
            }
            catch(Exception ex)
            {
                HasAdditionalSessions = false;  
                Logger.Report(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        MenuItem selectedFollowItem;
        public MenuItem SelectedFollowItem
        {
            get { return selectedFollowItem; }
            set
            {
                selectedFollowItem = value;
                OnPropertyChanged();
                if (selectedFollowItem == null)
                    return;

                LaunchBrowserCommand.Execute(selectedFollowItem.Parameter);

                SelectedFollowItem = null;
            }
        }

        Session selectedSession;
        public Session SelectedSession
        {
            get { return selectedSession; }
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

        ICommand  favoriteCommand;
        public ICommand FavoriteCommand =>
        favoriteCommand ?? (favoriteCommand = new Command<Session>((s) =>  ExecuteFavoriteCommand(s))); 

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
                            if(!result)
                                return;

                            var toggled = await FavoriteService.ToggleFavorite(session);
                            if(toggled)
                                await ExecuteLoadSessionsCommandAsync();
                        })
                });

        }

    }
}


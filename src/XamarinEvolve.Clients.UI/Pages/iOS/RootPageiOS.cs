using Xamarin.Forms;
using XamarinEvolve.Clients.UI;
using FormsToolkit;
using XamarinEvolve.Clients.Portable;
using XamarinEvolve.DataStore.Abstractions;

namespace XamarinEvolve.Clients.UI
{
    public class RootPageiOS : TabbedPage
    {

        public RootPageiOS()
        {

            #if ENABLE_TEST_CLOUD
            if (Settings.Current.Email == "xtc@xamarin.com")
            {
                Settings.Current.FirstRun = true;
                Settings.Current.FirstName = string.Empty;
                Settings.Current.LastName = string.Empty;
                Settings.Current.Email = string.Empty;
            }
            #endif

            NavigationPage.SetHasNavigationBar(this, false);
            Children.Add(new EvolveNavigationPage(new FeedPage()));
            Children.Add(new EvolveNavigationPage(new SessionsPage()));
            Children.Add(new EvolveNavigationPage(new EventsPage()));
            Children.Add(new EvolveNavigationPage(new MiniHacksPage()));
            Children.Add(new EvolveNavigationPage(new AboutPage()));

            MessagingService.Current.Subscribe<DeepLinkPage>("DeepLinkPage", async (m, p) =>
                {
                    switch(p.Page)
                    {
                        case AppPage.Notification:
                            NavigateAsync(AppPage.Notification);
                            await CurrentPage.Navigation.PopToRootAsync();
                            await CurrentPage.Navigation.PushAsync(new NotificationsPage());
                            break;
                        case AppPage.Events:
                            NavigateAsync(AppPage.Events);
                            await CurrentPage.Navigation.PopToRootAsync();
                            break;
                        case AppPage.MiniHacks:
                            NavigateAsync(AppPage.MiniHacks);
                            await CurrentPage.Navigation.PopToRootAsync();
                            break;
                        case AppPage.Session:
                            NavigateAsync(AppPage.Sessions);
                            var session = await DependencyService.Get<ISessionStore>().GetAppIndexSession (p.Id);
                            if (session == null)
                                break;
                            await CurrentPage.Navigation.PushAsync(new SessionDetailsPage(session));
                            break;
                    }

                });
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            switch (Children.IndexOf(CurrentPage))
            {
                case 0:
                    App.Logger.TrackPage(AppPage.Feed.ToString());
                    break;
                case 1:
                    App.Logger.TrackPage(AppPage.Sessions.ToString());
                    break;
                case 2:
                    App.Logger.TrackPage(AppPage.Events.ToString());
                    break;
                case 3:
                    App.Logger.TrackPage(AppPage.MiniHacks.ToString());
                    break;
                case 4:
                    App.Logger.TrackPage(AppPage.Information.ToString());
                    break;
            }
        }

        public void NavigateAsync(AppPage menuId)
        {
            switch ((int)menuId)
            {
                case (int)AppPage.Feed: CurrentPage = Children[0]; break;
                case (int)AppPage.Sessions: CurrentPage = Children[1]; break;
                case (int)AppPage.Events: CurrentPage = Children[2]; break;
                case (int)AppPage.MiniHacks: CurrentPage = Children[3]; break;
                case (int)AppPage.Information: CurrentPage = Children[4]; break;
                case (int)AppPage.Notification: CurrentPage = Children[0]; break;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();



            if (Settings.Current.FirstRun)
            {
                MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
            }
        }

       
    }
}



using System;

using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using Conference.Clients.Portable;
using FormsToolkit;
using Conference.DataStore.Abstractions;

namespace Conference.Clients.UI
{
    public class RootPageAndroid : MasterDetailPage
    {
        Dictionary<int, ConferenceNavigationPage> pages;
        DeepLinkPage page;
        bool isRunning = false;
        public RootPageAndroid()
        {
            pages = new Dictionary<int, ConferenceNavigationPage>();
            Master = new MenuPage(this);

            pages.Add(0, new ConferenceNavigationPage(new FeedPage()));

            Detail = pages[0];
            MessagingService.Current.Subscribe<DeepLinkPage>("DeepLinkPage", async (m, p) =>
                {
                    page = p;

                    if(isRunning)
                        await GoToDeepLink();
                });
        }



        public async Task NavigateAsync(int menuId)
        {
            ConferenceNavigationPage newPage = null;
            if (!pages.ContainsKey(menuId))
            {
                //only cache specific pages
                switch (menuId)
                {
                    case (int)AppPage.Feed: //Feed
                        pages.Add(menuId, new ConferenceNavigationPage(new FeedPage()));
                        break;
                    case (int)AppPage.Sessions://sessions
                        pages.Add(menuId, new ConferenceNavigationPage(new SessionsPage()));
                        break;
                    case (int)AppPage.Events://events
                        pages.Add(menuId, new ConferenceNavigationPage(new EventsPage()));
                        break;
                    case (int)AppPage.MiniHacks://Mini-Hacks
                        newPage = new ConferenceNavigationPage(new MiniHacksPage());
                        break;
                    case (int)AppPage.Sponsors://sponsors
                        newPage = new ConferenceNavigationPage(new SponsorsPage());
                        break;
                    case (int)AppPage.Venue: //venue
                        newPage = new ConferenceNavigationPage(new VenuePage());
                        break;
                    case (int)AppPage.ConferenceInfo://Conference info
                        newPage = new ConferenceNavigationPage(new ConferenceInformationPage());
                        break;
                    case (int)AppPage.FloorMap://Floor Maps
                        newPage = new ConferenceNavigationPage(new FloorMapsPage());
                        break;
                    case (int)AppPage.Settings://Settings
                        newPage = new ConferenceNavigationPage(new SettingsPage());
                        break;
                    case (int)AppPage.Evals:
                        newPage = new ConferenceNavigationPage (new EvaluationsPage ());
                        break;
                }
            }

            if(newPage == null)
                newPage = pages[menuId];
            
            if(newPage == null)
                return;

            //if we are on the same tab and pressed it again.
            if (Detail == newPage)
            {
                await newPage.Navigation.PopToRootAsync();
            }

            Detail = newPage;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            if (Settings.Current.FirstRun)
            {
                MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
            }

            isRunning = true;

            await GoToDeepLink();

        }
        async Task GoToDeepLink()
        {
            if (page == null)
                return;
            var p = page.Page;
            var id = page.Id;
            page = null;
            switch(p)
            {
                case AppPage.Sessions:
                    await NavigateAsync((int)AppPage.Sessions);
                    break;
                case AppPage.Session:
                    await NavigateAsync((int)AppPage.Sessions);
                    if (string.IsNullOrWhiteSpace(id))
                        break;

                    var session = await DependencyService.Get<ISessionStore>().GetAppIndexSession(id);
                    if (session == null)
                        break;
                    await Detail.Navigation.PushAsync(new SessionDetailsPage(session));
                    break;
                case AppPage.Events:
                    await NavigateAsync((int)AppPage.Events);
                    break;
                case AppPage.MiniHacks:
                    await NavigateAsync((int)AppPage.MiniHacks);
                    break;
            }

        }

    }
}



using System;
using Conference.DataObjects;
using System.Threading.Tasks;
using Xamarin.Forms;
using Conference.Clients.Portable;
using Conference.DataStore.Abstractions;
using FormsToolkit;
using System.Linq;

namespace Conference.Clients.Portable
{
    public class FavoriteService
    {
        Session sessionQueued;
        public FavoriteService()
        {
            MessagingService.Current.Subscribe(MessageKeys.LoggedIn, async (s) =>
                {
                    if(sessionQueued == null)
                        return;

                    await ToggleFavorite(sessionQueued);
                });
        }
        public async Task<bool> ToggleFavorite(Session session)
        {
            if(!Settings.Current.IsLoggedIn)
            {
                sessionQueued = session;
                DependencyService.Get<ILogger>().TrackPage(AppPage.Login.ToString(), "Favorite");
                MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
                return false;
            }

            sessionQueued = null;

            var store = DependencyService.Get<IFavoriteStore>();
            session.IsFavorite = !session.IsFavorite;//switch first so UI updates :)
            if (!session.IsFavorite)
            {
                DependencyService.Get<ILogger>().Track(ConferenceLoggerKeys.FavoriteRemoved, "Title", session.Title);

                var items = await store.GetItemsAsync ();
                foreach (var item in items.Where (s => s.SessionId == session.Id)) 
                {
                    await store.RemoveAsync (item);
                }
            }
            else
            {
                DependencyService.Get<ILogger>().Track(ConferenceLoggerKeys.FavoriteAdded, "Title", session.Title);
                await store.InsertAsync(new Favorite{ SessionId = session.Id });
            }

            Settings.Current.LastFavoriteTime = DateTime.UtcNow;
            return true;
        }
    }
}


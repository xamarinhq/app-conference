using System;
using System.Threading.Tasks;

namespace XamarinEvolve.DataStore.Abstractions
{
    public interface IStoreManager
    {
        bool IsInitialized { get; }
        Task InitializeAsync();
        ICategoryStore CategoryStore { get; }
        IFavoriteStore FavoriteStore { get; }
        IFeedbackStore FeedbackStore { get; }
        ISessionStore SessionStore { get; }
        ISpeakerStore SpeakerStore { get; }
        ISponsorStore SponsorStore { get; }
        IEventStore EventStore { get; }
        IMiniHacksStore MiniHacksStore { get; }
        INotificationStore NotificationStore { get; }

        Task<bool> SyncAllAsync(bool syncUserSpecific);
        Task DropEverythingAsync();
    }
}




namespace Conference.Clients.Portable
{
    public class DeepLinkPage
    {
        public AppPage Page { get; set; }
        public string Id { get; set;}
    }
    public enum AppPage
    {
        Feed,
        Sessions,
        Events,
        MiniHacks,
        Sponsors,
        Venue,
        FloorMap,
        ConferenceInfo,
        Settings,
        Session,
        Speaker,
        Sponsor,
        Login,
        Event,
        Notification,
        TweetImage,
        WiFi,
        CodeOfConduct,
        Filter,
        Information,
        Tweet,
        Evals
    }
}



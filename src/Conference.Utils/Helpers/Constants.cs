using System;

namespace Conference.Clients.Portable
{
   
    public static class ApiKeys
    {
        

        public const string AzureServiceBusName = "AzureServiceBusName";
        public const string AzureServiceBusUrl = "AzureServiceBusUrl"; // EX: sb://techdays-2016.servicebus.windows.net/
        public const string AzureKey ="AzureKey";  // TODO: take from your Azure portal

        public const string AzureHubName = "LaComarcaDO";
        public const string AzureListenConneciton = "Endpoint=sb://conferencetest.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=nC00PmSAZu5jv1v8qRGBvS6PwpiW9R5V7CrMhf0lWiA=";


        public const string GoogleSenderId ="xamarinawareness"; // TODO: take from your Google Developer Console
        public const string BingMapsUWPKey = "";
    }

    public static class EventInfo
    {
        public const string EventShortName = "LaComarca";
        public const string EventName = "LaComarcaDO";
        public const string Address1 = "";
        public const string Address2 = "";
        public const string VenueName = "";
        public const string VenuePhoneNumber = "";
        public const double Latitude = 52.340681d;
        public const double Longitude = 4.889541d;
        public const string TimeZoneName = "Europe/Amsterdam"; //https://en.wikipedia.org/wiki/List_of_tz_database_time_zones
        public const string HashTag = "#LaComarcaDO";
        public const string WiFiSSIDDefault = "LaComarcaDO";
        public const string WiFiPassDefault = "";
        public const string WifiUrl = AboutThisApp.CdnUrl + "wifi.json";

        public const string MiniHackStaffMemberName = "Mini-Hack coach";
        public const string MiniHackUnlockTag = "LaComarcaDO";

        public const string TicketUrl = "https://techdays.msnlevents.net/techdays2016";
        public static readonly DateTime StartOfConference = new DateTime(2016, 10, 04, 6, 0, 0, DateTimeKind.Utc);
        public static readonly DateTime EndOfConference = new DateTime(2016, 10, 05, 15, 30, 0, DateTimeKind.Utc);
    }

    public static class AboutThisApp
    {
        public const string AppLinkProtocol = "lacomarca";
        public const string PackageName = "do.lacomarca.app";
        public const string AppName = "TechDays16";
        public const string CompanyName = "LaComarca";
        public const string Developer = "Community";
        public const string DeveloperWebsite = "https://lacomarca.do";
        public const string OpenSourceUrl = "http://tiny.cc/app-evolve";
        public const string TermsOfUseUrl = "http://go.microsoft.com/fwlink/?linkid=206977";
        public const string PrivacyPolicyUrl = "http://go.microsoft.com/fwlink/?LinkId=521839";
        public const string OpenSourceNoticeUrl = "http://tiny.cc/app-evolve-osn";
        public const string EventRegistrationPage = "";
        public const string CdnUrl = ""; // TODO: set up your own CDN for static content
        public const string AppLinksBaseDomain = "www.lacomarca.do"; // TODO: use the domain name of the site you want to integrate AppLinks with
        public const string SessionsSiteSubdirectory = "Sessies";
        public const string SpeakersSiteSubdirectory = "Sprekers";
        public const string SponsorsSiteSubdirectory = "Sponsors";
        public const string MiniHacksSiteSubdirectory = "MiniHacks";
        public const string Copyright = "Copyright 2016 - TechDays";
        public const string CodeOfConductPageTitle = "Permission to be filmed";

        public const string Credits = "The TechDays 2016 mobile apps were handcrafted by Xpirit, based on the great work done by Xamarin.\n\n" +
            "Xpirit Team:\n" +
            "Roy Cornelissen\n" +
            "Marcel de Vries\n" +
            "Geert van der Cruijsen\n\n" +
            "Team TechDays NL\n\n" +
            "Many thanks to the original Xamarin Team:\n" +
            "James Montemagno\n" +
            "Pierce Boggan\n" +
           "\n" +
            "...and of course you! <3";
    }

    public static class PublicationSettings
    {
        public const uint iTunesAppId = 1111111111; // Your iTunes app ID
    }

    public static class MessageKeys
    {
        public const string NavigateToEvent = "navigate_event";
        public const string NavigateToSession = "navigate_session";
        public const string NavigateToSpeaker = "navigate_speaker";
        public const string NavigateToSponsor = "navigate_sponsor";
        public const string NavigateToImage = "navigate_image";
        public const string NavigateLogin = "navigate_login";
        public const string Error = "error";
        public const string Connection = "connection";
        public const string LoggedIn = "loggedin";
        public const string Message = "message";
        public const string Question = "question";
        public const string Choice = "choice";
    }
}


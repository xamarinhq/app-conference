using System;
using System.Collections.Generic;
using System.Text;

namespace Conference.Utils.Helpers
{
    public static class FeatureFlags
    {
        public static bool LoginEnabled => true; // Set to true to use original login provider; false means that anonymous accounts are used
        public static bool MiniHacksEnabled => false;
        public static bool EventsEnabled => true;
        public static bool ShowBuyTicketButton => false;
        public static bool ShowConferenceFeedbackButton => false;
        public static bool SpeakersEnabled => true;
        public static bool SponsorsOnTabPage => false;

        public static bool WifiEnabled => false;
        public static bool EvalEnabled => false;
        public static bool CodeOfConductEnabled => true;
        public static bool VenueEnabled => false;
        public static bool FloormapEnabled => false;
        public static bool AppLinksEnabled => true;
        public static bool ConferenceInformationEnabled => true;
        public static bool AppToWebLinkingEnabled => true;
        public static bool ShowLocationInSessionCell => true;

        /// <summary>
        /// Out of the box the Conference Mobile app uses sample data provided by the <see cref="Conference.DataStore.Mock"/>. 
        /// This is great for development, but you can also test against the test/development read-only Azure App Server Mobile Apps backend
        /// </summary>
        /// <remarks>
        /// // Simply change:
        /// public static bool UseMocks => true;
        /// 
        /// // to:
        /// public static bool UseMocks => false;
        /// </remarks>
        public static bool UseMocks => false;

        public static bool HockeyAppEnabled => false;
        public static bool GoogleAnalyticsEnabled => false;
    }
}

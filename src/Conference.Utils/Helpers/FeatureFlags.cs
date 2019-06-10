using System;
using System.Collections.Generic;
using System.Text;

namespace Conference.Utils.Helpers
{
    public static class FeatureFlags
    {
        public static bool LoginEnabled => false; // Set to true to use original login provider; false means that anonymous accounts are used
        public static bool MiniHacksEnabled => true;
        public static bool EventsEnabled => false;
        public static bool ShowBuyTicketButton => true;
        public static bool ShowConferenceFeedbackButton => true;
        public static bool SpeakersEnabled => true;
        public static bool SponsorsOnTabPage => false;

        public static bool WifiEnabled => false;
        public static bool EvalEnabled => false;
        public static bool CodeOfConductEnabled => true;
        public static bool FloormapEnabled => true;
        public static bool AppLinksEnabled => true;
        public static bool ConferenceInformationEnabled => false;
        public static bool AppToWebLinkingEnabled => true;
        public static bool ShowLocationInSessionCell => true;

        public static bool UseMocks => true;
        public static bool HockeyAppEnabled => true;
        public static bool GoogleAnalyticsEnabled => true;
    }
}

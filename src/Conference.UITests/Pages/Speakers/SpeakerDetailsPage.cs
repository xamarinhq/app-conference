using System;
using NUnit.Framework;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace Conference.UITests
{
    public class SpeakerDetailsPage : BasePage
    {
        readonly string MoreSessions = "MoreSessionsSection";

        public SpeakerDetailsPage()
            : base ("Speaker Info", "Speaker Info")
        {
        }

        public SpeakerDetailsPage ValidateAdditionalSessions(bool yesorno)
        {
            try
            {
                app.ScrollDownTo(MoreSessions, timeout:TimeSpan.FromSeconds(5));
                if (!yesorno)
                    Assert.IsTrue(false, message: "Expected no additional sessions, but found more");
            }
            catch
            {
                app.Screenshot("No Additional Sessions Found");
                if (yesorno)
                    Assert.IsTrue(false, message: "Expected additional sessions, but found none");
            }

            app.Screenshot("Session Numbers Verified");

            return this;
        }
    }
}


using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace XamarinEvolve.UITests
{
    public class EventsTests : AbstractSetup
    {
        public EventsTests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        public void NavigateToEvent()
        {
            new FeedPage()
                .NavigateTo("Events");

            new EventsPage()
                .VerifyContentPresent()
                .SelectEvent("Darwin Lounge");

            new EventDetailsPage()
                .VerifyContentPresent();
        }

        [Test]
        public void HappyHourSponsorCheck()
        {
            new FeedPage()
                .NavigateTo("Events");

            new EventsPage()
                .SelectEvent("Happy Hour");

            new EventDetailsPage()
                .VerifySponsorPresent()
                .SelectSponsor();

            new SponsorDetailsPage()
                .VerifyContentPresent();

        }
    }
}


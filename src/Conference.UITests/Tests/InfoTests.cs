using System;
using Xamarin.UITest;
using NUnit.Framework;

namespace Conference.UITests
{
    public class InfoTests : AbstractSetup
    {
        public InfoTests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        public void NavigateToSponsor()
        {
            if (OnAndroid)
            {
                new FeedPage()
                    .NavigateTo("Sponsors");
            }

            if (OniOS)
            {
                new FeedPage()
                    .NavigateTo("Info");

                new InfoPage()
                    .NavigateToInfoItem("Sponsors");
            }

            new SponsorsPage()
                .VerifyContentPresent()
                .SelectSponsor("Dropbox");

            new SponsorDetailsPage()
                .VerifyContentPresent();
        }

        [Test]
        public void WifiSetup()
        {
            if (OnAndroid)
            {
                new FeedPage()
                    .NavigateTo("Conference Info");

                new WiFiInformationPage()
                    .SetUpWifi()
                    .CopyPasswords();
            }

            if (OniOS)
            {
                new FeedPage()
                    .NavigateTo("Info");

                new InfoPage()
                    .NavigateToInfoItem("Wi-Fi Information");

                new WiFiInformationPage()
                    .CopyPasswords();
            }
        }
    }
}


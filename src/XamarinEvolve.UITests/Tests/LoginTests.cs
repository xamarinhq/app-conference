using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace XamarinEvolve.UITests
{
    public class LoginTests : AbstractSetup
    {
        public LoginTests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        public void LoginFromMenu()
        {
            if (OnAndroid)
            {
                new FeedPage()
                    .NavigateTo("Settings");

                new SettingsPage()
                    .TapSignIn();

                new LoginPage()
                    .EnterCredentials("xtc@xamarin.com", "fake")
                    .TapLogin();

                new SettingsPage()
                    .ConfirmedLoggedIn();
            }
            if (OniOS)
            {
                new FeedPage()
                    .NavigateTo("Info");

                new InfoPage()
                    .TapSignIn();

                new LoginPage()
                    .EnterCredentials("xtc@xamarin.com", "fake")
                    .TapLogin();

                new InfoPage()
                    .ConfirmedLoggedIn();
            }


        }
    }
}


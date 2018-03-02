using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Conference.UITests
{
        public class FeedTests : AbstractSetup
        {
                public FeedTests(Platform platform)
                    : base(platform)
                {
                    }

                [Test]
                public void AnnouncementVerify()
                {
                        new FeedPage()
                            .SelectAnnouncement();

                        new NotificationsPage()
                            .SelectAnnouncementItem();


                    }

                [Test]
                public void SocialVerify()
                {
                        new FeedPage()
                            .SelectSocialItem();
                    }

                [Test]
                public void FavoriteVerify()
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
                                .ConfirmedLoggedIn()
                                .NavigateTo("Sessions");
                
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
                                .ConfirmedLoggedIn()
                                .NavigateTo("Sessions");
                        }

                        
                        new SessionsPage()
                            .FavoriteFirstSession();

                        new SessionsPage()
                            .NavigateTo("Conference Feed");

                        new FeedPage()
                            .SelectFavorite();

                        new SessionsDetailsPage();

                    }
            }
}

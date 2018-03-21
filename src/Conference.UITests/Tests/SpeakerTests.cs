using System;
using Xamarin.UITest;
using NUnit.Framework;

namespace Conference.UITests
{
    public class SpeakerTests : AbstractSetup
    {
        public SpeakerTests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        public void MultipleSessions()
        {
            new FeedPage().NavigateTo("Sessions");

            new SessionsPage()
                .InvestigateFirstSession();

            new SessionsDetailsPage()
                .GoToSpeakerDetails();

            new SpeakerDetailsPage()
                .ValidateAdditionalSessions(true);
        }

        [Test]
        public void SingleSession()
        {
            new FeedPage().NavigateTo("Sessions");

            new SessionsPage()
                .InvestigateSessionMarked("Everyone can create beautiful apps with material design");

            new SessionsDetailsPage()
                .GoToSpeakerDetails();

            new SpeakerDetailsPage()
                .ValidateAdditionalSessions(false);
        }

    }
}


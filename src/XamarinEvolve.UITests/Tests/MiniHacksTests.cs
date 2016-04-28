using System;
using Xamarin.UITest;
using NUnit.Framework;

namespace XamarinEvolve.UITests
{
    public class MiniHacksTests : AbstractSetup
    {
        public MiniHacksTests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        public void InvestigateMiniHack()
        {
            var title = OniOS ? "Mini-Hacks" : "Mini-hacks";
            new FeedPage().NavigateTo(title);

            new MiniHacksPage()
                .SelectMiniHack("Xamarin.Forms");

            new MiniHackDetailsPage()
                .VerifyInfo();
        }

        [Test]
        public void MiniHackDirections()
        {
            var title = OniOS ? "Mini-Hacks" : "Mini-hacks";
            new FeedPage().NavigateTo(title);

            new MiniHacksPage()
                .SelectMiniHack("HockeyApp");

            new MiniHackDetailsPage()
                .GoToMiniHackDirections();
        }

        [Test]
        public void FinishMiniHack()
        {
            var title = OniOS ? "Mini-Hacks" : "Mini-hacks";
            new FeedPage().NavigateTo(title);

            new MiniHacksPage()
                .SelectMiniHack("Bitrise");

            new MiniHackDetailsPage()
                .FinishMiniHack();
        }
    }
}


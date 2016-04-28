using System;
using NUnit.Framework;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinEvolve.UITests
{
    public class MiniHackDetailsPage : BasePage
    {
        readonly string HackName = "HackName";
        readonly string HackSubtitle = "HackSubtitle";
        readonly string HackDescription = "HackDescription";
        readonly string HackDirections = "MiniHackDirections";
        readonly string FinishButton = "FinishButton";

        public MiniHackDetailsPage()
            : base("Mini-Hack Details", "Mini-Hack Details")
        {
        }

        public MiniHackDetailsPage VerifyInfo()
        {
            Assert.IsNotNull(app.Query(HackName)[0].Text, "Hack Name Not Found");
            Assert.IsNotNull(app.Query(HackSubtitle)[0].Text, "Hack Subtitle Not Found");
            app.ScrollDownTo(HackDescription);
            Assert.IsNotNull(app.Query(HackDescription)[0].Text, "Hack Description Not Found");
            app.ScrollDownTo(HackDirections);
            Assert.IsNotNull(app.Query(HackDirections)[0], "Hack Directions Not Found");
            app.ScrollDownTo(FinishButton);
            Assert.IsNotNull(app.Query(FinishButton)[0], "Finish Button Not Found");
            app.Screenshot("Mini-Hack information verified");

            return this;
        }

        public void GoToMiniHackDirections()
        {
            app.ScrollDownTo(HackDirections);
            app.Tap(HackDirections);
            app.Screenshot("Opened up Mini-Hack directions");
        }

        public void FinishMiniHack()
        {
            app.ScrollDownTo(FinishButton);
            app.Screenshot("About to tap: 'Finish Mini-Hack'");
            app.Tap(FinishButton);
            System.Threading.Thread.Sleep(3000);
            app.Screenshot("Tapped: 'Finish Mini-Hack'");
        }
    }
}


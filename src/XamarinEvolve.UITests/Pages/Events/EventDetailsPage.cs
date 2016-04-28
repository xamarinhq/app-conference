using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;
using System.Linq;

namespace XamarinEvolve.UITests
{
    public class EventDetailsPage : BasePage
    {
        readonly string EventTitle = "EventTitle";
        readonly string EventTime = "EventTime";
        readonly string EventDescription = "EventDescription";
        readonly string LoacationName = "EventLocationRoom";
        readonly string ReminderButton;
        readonly string SponsorName = "SponsorName";
        readonly string SponsorImage = "SponsorImage";
        readonly string SponsorLevel = "SponsorLevel";
        readonly string SponsorCell = "SponsorCell";

        public EventDetailsPage()
            : base(x => x.Class("Toolbar").Descendant().Text("Event Details"), x => x.Class("UINavigationBar").Id("Event Details"))
        {
            if (OnAndroid)
            {
                ReminderButton = "AndroidReminderButton";
            }
            if (OniOS)
            {
                ReminderButton = "iOSReminderButton";
            }
        }

        public EventDetailsPage VerifyContentPresent()
        {
            app.WaitForElement(EventTitle);

            Assert.IsNotNull(app.Query(EventTitle).First().Text);
            Assert.IsNotNull(app.Query(EventTime).First().Text);
            Assert.IsNotNull(app.Query(EventDescription).First().Text);
            Assert.IsNotNull(app.Query(LoacationName).First().Text);

            return this;
        }

        public EventDetailsPage VerifySponsorPresent()
        {
            Assert.IsNotNull(app.Query(SponsorName).First().Text);
            Assert.IsNotNull(app.Query(SponsorLevel).First().Text);
            app.WaitForElement(SponsorImage);
            Assert.IsNotNull(app.Query(SponsorImage).First().Description);

            return this;
        }

        public void SelectSponsor()
        {
            app.Screenshot("Selecting Sponsor");
            app.WaitForElement(SponsorCell);
            app.Tap(SponsorCell);
        }

    }
}


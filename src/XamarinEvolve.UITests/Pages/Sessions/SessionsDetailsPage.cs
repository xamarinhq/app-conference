using System;
using NUnit.Framework;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;
using System.Threading;

namespace XamarinEvolve.UITests
{
    public class SessionsDetailsPage : BasePage
    {
        readonly string SessionTitle = "SessionTitle";
        readonly string SessionDate = "SessionDate";
        readonly string SessionAbstract = "SessionAbstract";
        readonly string SessionSpeakers = "SessionSpeakers";
        readonly string RateButton = "Rate this Session";
        readonly string SubmitButton = "Submit";
        readonly string FeedbackMessage = "Thanks for the feedback, have a great Evolve.";
        readonly Query FirstSpeaker;
        readonly string StarImage;

        public SessionsDetailsPage()
            : base("Session Details", "Session Details")
        {
            if (OnAndroid)
            {
                FirstSpeaker = x => x.Marked("SessionSpeakers").Descendant().Class("FormsTextView").Index(0);
                StarImage = "FormsImageView";
            }
            if (OniOS)
            {
                FirstSpeaker = x => x.Id("SessionSpeakers").Descendant().Class("Xamarin_Forms_Platform_iOS_ViewCellRenderer_ViewTableCell").Index(0);
                StarImage = "UIImageView";
            }
        }

        public Query StarNumber(int number)
        {
            int newnumber = OniOS ? (number - 1) * 2 : number * 2;
            return x => x.Class(StarImage).Index(newnumber);
        }

        public SessionsDetailsPage VerifyContentPresent()
        {
            Assert.IsNotNull(app.Query(SessionTitle)[0].Text, "Session Title Not Found");
            Assert.IsNotNull(app.Query(SessionDate)[0].Text, "Session Date Not Found");
            app.ScrollDownTo(SessionAbstract);
            Assert.IsNotNull(app.Query(SessionAbstract)[0].Text, "Session Abstract Not Found");
            app.ScrollDownTo(SessionSpeakers);
            Assert.IsNotNull(app.Query(SessionSpeakers)[0], "Session Speakers Not Found");
            app.Screenshot("Session information verified as present");

            return this;
        }

        public void GoToSpeakerDetails()
        {
            app.ScrollDownTo("FeedbackTitle", timeout:TimeSpan.FromSeconds(10));
            app.Screenshot("Scrolled Down to Session Speakers");
            app.Tap(FirstSpeaker);
            app.Screenshot("Selected first speaker");
        }

        public SessionsDetailsPage RateThisSession()
        {
            app.ScrollDown();
            app.Tap(RateButton);
            app.Screenshot("Tapped: 'Rate This Session'");
            return this;
        }

        public SessionsDetailsPage VerifyStarsIncrementally()
        {
            app.Tap(StarNumber(1)); 
            app.WaitForElement(x => x.Text("Not a fan"));
            app.Screenshot("1 star message");
            app.Tap(StarNumber(2));
            app.WaitForElement(x => x.Text("It was ok"));
            app.Screenshot("2 star message");
            app.Tap(StarNumber(3));
            app.WaitForElement(x => x.Text("Good"));
            app.Screenshot("3 star message");
            app.Tap(StarNumber(4));
            app.WaitForElement(x => x.Text("Great"));
            app.Screenshot("4 star message");
            app.Tap(StarNumber(5));
            app.WaitForElement(x => x.Text("Love it!"));
            app.Screenshot("5 star message");

            return this;
        }

        public SessionsDetailsPage SubmitReview()
        {
            app.Tap(SubmitButton);
            app.WaitForElement(FeedbackMessage);
            app.Screenshot("Feedback dialog appears");

            app.Tap("OK");

            return this;
        }

        public void FeedbackReceived()
        {
            app.WaitForElement("Thanks for your feedback!");
            app.Screenshot("Feedback received");
        }
    }
}


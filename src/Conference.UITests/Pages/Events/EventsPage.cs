using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Linq;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace Conference.UITests
{
    public class EventsPage : BasePage
    {
        readonly string EventItemTitle = "EventItemTitle";
        readonly string EventItemTime = "EventItemTime";
        readonly string EventItemCircleDate = "EventItemCircleDate";
        readonly string EventItemDay = "EventItemDay";
        readonly Func<string, Query> EventNamed = name => x => x.Marked("EventItemTitle").Text(name);

        public EventsPage()
            : base(x => x.Class("Toolbar").Descendant().Raw("* {text LIKE 'Events (*)'}"), x => x.Id("EventsPageIdentifier"))
        {
        }

        public void SelectEvent(string name)
        {
            app.ScrollDownTo(EventNamed(name));
            app.Tap(EventNamed(name));
        }

        public EventsPage VerifyContentPresent()
        {
            Assert.IsNotNull(app.Query(EventItemTitle).First().Text);
            Assert.IsNotNull(app.Query(EventItemTime).First().Text);
            Assert.IsNotNull(app.Query(EventItemCircleDate).First().Text);
            Assert.IsNotNull(app.Query(EventItemDay).First().Text);

            return this;
        }
    }
}


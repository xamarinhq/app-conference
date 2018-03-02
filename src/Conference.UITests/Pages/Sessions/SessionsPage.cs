using System;
using NUnit.Framework;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;
using System.Linq;
using Xamarin.UITest.Android;

namespace Conference.UITests
{
    public class SessionsPage : BasePage
    {
        readonly Query SearchField;
        readonly Query SessionCellContainer;
        readonly Query FilterButton;
        readonly Query FavoriteButton;

        readonly string LoadingMessage = "Loading Sessions...";

        public SessionsPage()
            : base ("Sessions", "Sessions")
        {
            if (OnAndroid)
            {
                SearchField = x => x.Id("search_src_text");
                SessionCellContainer = x => x.Class("ViewCellRenderer_ViewCellContainer").Index(1);
                FilterButton = x => x.Marked("Filter");
                FavoriteButton = x => x.Marked("FavoriteButton");
            }
            if (OniOS)
            {
                SearchField = x => x.Class("UISearchBarTextField");
                SessionCellContainer = x => x.Class("Xamarin_Forms_Platform_iOS_ViewCellRenderer_ViewTableCell");
                FilterButton = x => x.Class("UINavigationButton").Marked("Filter");
                FavoriteButton = x => x.Marked("FavoriteButton");
            }
        }

        public SessionsPage EnterSearchAndVerify(string search, bool valid)
        {
            app.Tap(SearchField);
            app.EnterText(search);
            app.Screenshot("Entered text: " + search);
            app.DismissKeyboard();

            app.WaitForNoElement(LoadingMessage);

            bool result = app.Query(SessionCellContainer).Any();
            Assert.IsTrue(result == valid, String.Format("Expected search to be {0}, but was {1}", valid, result));
            app.Screenshot("Session search: " + search);

            if (!valid)
                Assert.IsNotEmpty(app.Query("No Sessions Found"));

            return this;
        }

        public void FavoriteFirstSession()
        {
            app.Tap(FavoriteButton);
            app.Screenshot("Tapped Favorite Button");
        }

        public void GoToFilterSessionsPage()
        {
            app.Screenshot("Tapping Filter Button");
            app.Tap(FilterButton);
        }

        public void InvestigateFirstSession()
        {
            app.Screenshot("Investigating First Session");
            app.Tap(SessionCellContainer);
        }

        public void InvestigateSessionMarked(string title)
        {
            app.Screenshot("Selecting session: " + title);
            app.Tap(title);
        }

        public void ValidateFavorite()
        {
            app.Screenshot("Favorite Button is filled in");
        }
    }
}


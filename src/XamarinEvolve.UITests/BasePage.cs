using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;
using Xamarin.UITest.iOS;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;
using System.Linq;

namespace XamarinEvolve.UITests
{
    public class BasePage
    {
        protected readonly IApp app;
        protected readonly bool OnAndroid;
        protected readonly bool OniOS;

        protected Func<AppQuery, AppQuery> Trait;

        protected BasePage()
        {
            app = AppInitializer.App;

            OnAndroid = app.GetType() == typeof(AndroidApp);
            OniOS = app.GetType() == typeof(iOSApp);

            InitializeCommonQueries();
        }

        protected BasePage(Func<AppQuery, AppQuery> androidTrait, Func<AppQuery, AppQuery> iOSTrait)
            : this()
        {
            if (OnAndroid)
                Trait = androidTrait;
            if (OniOS)
                Trait = iOSTrait;

            AssertOnPage(TimeSpan.FromSeconds(30));

            app.Screenshot("On " + this.GetType().Name);
        }

        protected BasePage(string androidTrait, string iOSTrait)
            : this(x => x.Marked(androidTrait), x => x.Marked(iOSTrait))
        {
        }

        /// <summary>
        /// Verifies that the trait is still present. Defaults to no wait.
        /// </summary>
        /// <param name="timeout">Time to wait before the assertion fails</param>
        protected void AssertOnPage(TimeSpan? timeout = default(TimeSpan?))
        {
            if (Trait == null)
                throw new NullReferenceException("Trait not set");

            var message = "Unable to verify on page: " + this.GetType().Name;

            if (timeout == null)
                Assert.IsNotEmpty(app.Query(Trait), message);
            else
                Assert.DoesNotThrow(() => app.WaitForElement(Trait, timeout: timeout), message);
        }

        /// <summary>
        /// Verifies that the trait is no longer present. Defaults to a two second wait.
        /// </summary>
        /// <param name="timeout">Time to wait before the assertion fails</param>
        protected void WaitForPageToLeave(TimeSpan? timeout = default(TimeSpan?))
        {
            if (Trait == null)
                throw new NullReferenceException("Trait not set");

            timeout = timeout ?? TimeSpan.FromSeconds(2);
            var message = "Unable to verify *not* on page: " + this.GetType().Name;

            Assert.DoesNotThrow(() => app.WaitForNoElement(Trait, timeout: timeout), message);
        }

        #region CommonPageActions

        // Use this region to define functionality that is common across many or all pages in your app.
        // Eg tapping the back button of a page or selecting the tabs of a tab bar

        Query Hamburger;
        Func<string, Query> Tab;

        void InitializeCommonQueries()
        {
            if (OnAndroid)
            {
                Hamburger = x => x.Class("ImageButton").Marked("OK");
                Tab = name => x => x.Id("design_menu_item_text").Text(name);
            }
            if (OniOS)
            {
                Tab = name => x => x.Class("UITabBarButtonLabel").Text(name);
            }
        }

        public void NavigateTo(string tabName)
        {
            if (OnAndroid)
            {
                if (app.Query(Hamburger).Any())
                    app.Tap(Hamburger);

                app.Screenshot("Navigation Menu Open");
                int count = 0;
                while (!app.Query(tabName).Any() && count < 3)
                {
                    app.ScrollDown(x => x.Class("NavigationMenuView"));
                    count++;
                }
            }
            app.Tap(Tab(tabName));
        }

        public void PullToRefresh()
        {
            var rect = app.Query().First().Rect;
            app.DragCoordinates(rect.CenterX, rect.CenterY, rect.CenterX, rect.Height);
        }

        #endregion
    }
}


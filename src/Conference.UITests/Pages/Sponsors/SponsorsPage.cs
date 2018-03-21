using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Linq;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace Conference.UITests
{
    public class SponsorsPage : BasePage
    {
        readonly string SponsorName = "SponsorName";
        readonly string SponsorLevel = "SponsorLevel";
        readonly string SponsorImage = "SponsorImage";
        readonly string LoadingText = "Loading Sponsors...";
        readonly Func<string, Query> SponsorNamed = name => x => x.Marked("SponsorName").Text(name);

        public SponsorsPage()
            : base(x => x.Class("Toolbar").Descendant().Text("Sponsors"), x => x.Class("UINavigationBar").Id("Sponsors"))
        {
            app.WaitForNoElement(LoadingText);
        }

        public void SelectSponsor(string name)
        {
            app.ScrollDownTo(SponsorNamed(name));
            app.Screenshot($"Scrolled down to: '{name}'");
            app.Tap(SponsorNamed(name));
            app.Screenshot("Tapped Sponsor");
        }

        public SponsorsPage VerifyContentPresent()
        {
            Assert.IsNotNull(app.Query(SponsorName).First().Text);
            Assert.IsNotNull(app.Query(SponsorLevel).First().Text);

            app.WaitForElement(SponsorImage);
            Assert.IsNotEmpty(app.Query(SponsorImage));

            return this;
        }
    }
}


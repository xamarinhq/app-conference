using System;
using NUnit.Framework;
using System.Linq;

namespace Conference.UITests
{
    public class SponsorDetailsPage : BasePage
    {
        readonly string SponsorImage = "SponsorDetailImage";
        readonly string SponsorName = "SponsorDetailName";
        readonly string SponsorLevel = "SponsorDetailLevel";
        readonly string SponsorDescription = "SponsorDetailDescription";
        readonly string SponsorLinks = "SponsorDetailLinks";

        public SponsorDetailsPage()
            : base(x => x.Class("Toolbar").Descendant().Text("Sponsor Details"), x => x.Class("UINavigationBar").Id("Sponsor Details"))
        {
        }

        public SponsorDetailsPage VerifyContentPresent()
        {
            app.WaitForElement(SponsorImage);
            Assert.IsNotEmpty(app.Query(SponsorImage));
            Assert.IsNotNull(app.Query(SponsorName).First().Text);
            Assert.IsNotNull(app.Query(SponsorLevel).First().Text);
            Assert.IsNotNull(app.Query(SponsorDescription).First().Text);
            app.ScrollDownTo(SponsorLinks);
            Assert.IsNotEmpty(app.Query(SponsorLinks));

            return this;
        }
    }
}


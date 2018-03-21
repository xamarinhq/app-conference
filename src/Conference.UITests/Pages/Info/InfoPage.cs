using System;

namespace Conference.UITests
{
    public class InfoPage : BasePage
    {
        readonly string SignInButton = "Sign In";

        public InfoPage()
            : base(null, x => x.Class("UINavigationBar").Id("Info"))
        {
        }

        public void NavigateToInfoItem(string itemName)
        {
            app.ScrollDownTo(itemName);
            app.Tap(itemName);
        }

        public void TapSignIn()
        {
            app.WaitForElement(SignInButton);
            app.Tap(SignInButton);
        }

        public InfoPage ConfirmedLoggedIn()
        {
            app.WaitForElement("XTC User", "Timed out waiting for element", TimeSpan.FromMinutes(1));
            app.WaitForElement("Sign Out");
            return this;
        }
    }
}


using System;

namespace Conference.UITests
{
    public class SettingsPage : BasePage
    {
        readonly string SignInButton = "Sign In";

        public SettingsPage()
            : base ("Settings", "Settings")
        {
        }

        public void TapSignIn()
        {
            app.Tap(SignInButton);
        }

        public SettingsPage ConfirmedLoggedIn()
        {
            app.WaitForElement("Sign Out");

            return this;
        }
    }
}


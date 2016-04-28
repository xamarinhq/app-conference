using System;
using System.Threading;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinEvolve.UITests
{
    public class LoginPage : BasePage
    {
        readonly string EmailField = "EmailTextField";
        readonly string PasswordField = "PasswordTextField";
        readonly string NotNowButton = "NotNowButton";
        readonly string SignInButton = "SignInButton";

        public LoginPage()
            : base("LoginPageIdentifier", "LoginPageIdentifier")
        {
        }

        public LoginPage EnterCredentials(string email, string password)
        {
            app.Tap(EmailField);
            app.EnterText(email);
            app.DismissKeyboard();
            app.Screenshot(String.Format("Entered email: '{0}'", email));

            app.Tap(PasswordField);
            app.EnterText(password);
            app.DismissKeyboard();
            app.Screenshot("Entered password");

            return this;
        }

        public void TapLogin()
        {
            app.Tap(SignInButton);
        }

        public void TapNotNow()
        {
            app.Tap(NotNowButton);
        }
    }
}


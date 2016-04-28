using System;
using Xamarin.UITest;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamarinEvolve.UITests
{
    public class WiFiInformationPage : BasePage
    {
        readonly string StandardPassword = "2.4GHzPassword";
        readonly string EnhancedPassword = "5GHzPassword";
        readonly Query PasswordCopiedConfirmation;

        readonly string StandardSetUpButton;
        readonly string EnhancedSetUpButton;
        readonly string StandardSetUpConfirmation;
        readonly string EnhancedSetUpConfirmation;


        public WiFiInformationPage()
            : base(x => x.Class("Toolbar").Descendant().Text("Conference Information"), x => x.Class("UINavigationBar").Id("Wi-Fi Information"))
        {
            if(OnAndroid)
            {
                PasswordCopiedConfirmation = x => x.Text("Password Copied");

                StandardSetUpButton = "2.4GHzSetUpButton";
                EnhancedSetUpButton = "5GHzSetUpButton";
                StandardSetUpConfirmation = "2.4GHzSuccessText";
                EnhancedSetUpConfirmation = "5GHzSuccessText";
            }
            if(OniOS)
            {
                PasswordCopiedConfirmation = x => x.Class("UIButton").Child().Marked("Password Copied");
            }
        }

        public WiFiInformationPage CopyPasswords()
        {
            app.Tap(StandardPassword);
            app.WaitForElement(PasswordCopiedConfirmation);
            app.Screenshot("Coppied standard password");
            app.WaitForNoElement(PasswordCopiedConfirmation);

            return this;
        }

        public WiFiInformationPage SetUpWifi()
        {
            if(OnAndroid)
            {
                app.Tap(StandardSetUpButton);
                app.WaitForElement(StandardSetUpConfirmation);
                app.Screenshot("Set up standard network");

            }
            if(OniOS)
            {
                throw new NotImplementedException();
            }

            return this;
        }
    }
}


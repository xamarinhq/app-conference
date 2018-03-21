using System;
using Xamarin.UITest;
using NUnit.Framework;
using System.Linq;
using Xamarin.UITest.Android;
using Xamarin.UITest.iOS;

namespace Conference.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public abstract class AbstractSetup
    {
        protected IApp app;
        protected Platform platform;
        protected  bool OnAndroid;
        protected  bool OniOS;

        public AbstractSetup(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public virtual void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
            OnAndroid = app.GetType() == typeof(AndroidApp);
            OniOS = app.GetType() == typeof(iOSApp);

            if (app.Query("LoginPageIdentifier").Any())
            {
                new LoginPage().TapNotNow();
                System.Threading.Thread.Sleep(2000);
            }

            if (app.Query("Push Notifications").Any())
                app.Tap("Maybe Later");
        }
    }
}

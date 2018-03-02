using System;
using Xamarin.UITest;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace Conference.UITests
{
    public class NotificationsPage : BasePage
    {
        string AnnouncementItem = "NotificationItem";

        public NotificationsPage()
            : base (x => x.Id("toolbar").Child(0).Text("Announcements"), x => x.Class("UINavigationBar").Id("Announcements"))
        {
            if (OnAndroid)
            {
            }

            if (OniOS)
            {
            }
        }

        public void SelectAnnouncementItem()
        {
            app.WaitForElement(AnnouncementItem);
            app.Screenshot("Tapping on Announcement");
            app.Tap(AnnouncementItem);

        }
    }
}


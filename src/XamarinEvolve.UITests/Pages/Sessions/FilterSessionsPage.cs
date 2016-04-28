using System;

namespace XamarinEvolve.UITests
{
    public class FilterSessionsPage : BasePage
    {
        readonly string DoneButton = "Done";

        public FilterSessionsPage()
            : base ("Filter Sessions", "Filter Sessions")
        {
            if (OnAndroid)
            {
            }
            if (OniOS)
            {
            }
        }

        public FilterSessionsPage SelectSessionFilters(
            bool PastSessions = false, 
            bool FavoritesOnly = false)
        {
            if (OniOS)
            {
                app.Query(x => x.Class("UISwitch").Marked("Show Past Sessions").Invoke("setOn", PastSessions, "animated", true));
                app.Screenshot("'Show Past Sessions' set to: " + PastSessions);

                app.Query(x => x.Class("UISwitch").Marked("Show Favorites Only").Invoke("setOn", FavoritesOnly, "animated", true));
                app.Screenshot("'Show Favorites Only' set to: " + FavoritesOnly);
            }
            if (OnAndroid)
            {
                app.Query(x => x.Marked("Show Past Sessions").Parent(1).Descendant().Class("android.widget.Switch").Invoke("setChecked", PastSessions));
                app.Screenshot("'Show Past Sessions' set to: " + PastSessions);

                app.Query(x => x.Marked("Show Favorites Only").Parent(1).Descendant().Class("android.widget.Switch").Invoke("setChecked", FavoritesOnly));
                app.Screenshot("'Show Favorites Only' set to: " + FavoritesOnly);
            }

            return this;
        }

        public FilterSessionsPage SelectSessionCategories(
            bool DisplayAll = true, 
            bool Android = true, 
            bool iOS = true,
            bool XamarinForms = true,
            bool Design = true,
            bool Secure = true,
            bool Test = true,
            bool Monitor = true)
        {
            if (DisplayAll)
            {
                app.Screenshot("'Display All Selected, no other category filters enabled'");
                return this;
            }

            if (OniOS)
            {
                app.Query(x => x.Class("UISwitch").Marked("Display All").Invoke("setOn", DisplayAll, "animated", true));
                app.Screenshot("'Display All' set to: " + DisplayAll);

                app.Query(x => x.Class("UISwitch").Marked("Android").Invoke("setOn", Android, "animated", true));
                app.Screenshot("'Android' set to: " + Android);

                app.Query(x => x.Class("UISwitch").Marked("iOS").Invoke("setOn", iOS, "animated", true));
                app.Screenshot("'iOS' set to: " + iOS);

                app.Query(x => x.Class("UISwitch").Marked("Xamarin.Forms").Invoke("setOn", XamarinForms, "animated", true));
                app.Screenshot("'Xamarin.Forms' set to: " + XamarinForms);

                app.Query(x => x.Class("UISwitch").Marked("Design").Invoke("setOn", Design, "animated", true));
                app.Screenshot("'Design' set to: " + Design);

                app.Query(x => x.Class("UISwitch").Marked("Secure").Invoke("setOn", Secure, "animated", true));
                app.Screenshot("'Secure' set to: " + Secure);

                app.Query(x => x.Class("UISwitch").Marked("Test").Invoke("setOn", Test, "animated", true));
                app.Screenshot("'Test' set to: " + Test);

                app.Query(x => x.Class("UISwitch").Marked("Monitor").Invoke("setOn", Monitor, "animated", true));
                app.Screenshot("'Monitor' set to: " + Monitor);
            }
            if (OnAndroid)
            {
                app.Query(x => x.Marked("Display All").Parent(1).Descendant().Class("SwitchCompat").Invoke("setChecked", DisplayAll));
                app.Screenshot("'Display All' set to: " + DisplayAll);

                app.Query(x => x.Marked("Android").Parent(1).Descendant().Class("SwitchCompat").Invoke("setChecked", Android));
                app.Screenshot("'Android' set to: " + Android);

                app.Query(x => x.Marked("iOS").Parent(1).Descendant().Class("SwitchCompat").Invoke("setChecked", iOS));
                app.Screenshot("'iOS' set to: " + iOS);

                app.Query(x => x.Marked("Xamarin.Forms").Parent(1).Descendant().Class("SwitchCompat").Invoke("setChecked", XamarinForms));
                app.Screenshot("'Xamarin.Forms' set to: " + XamarinForms);

                app.Query(x => x.Marked("Design").Parent(1).Descendant().Class("SwitchCompat").Invoke("setChecked", Design));
                app.Screenshot("'Design' set to: " + Design);

                app.Query(x => x.Marked("Secure").Parent(1).Descendant().Class("SwitchCompat").Invoke("setChecked", Secure));
                app.Screenshot("'Secure' set to: " + Secure);

                app.Query(x => x.Marked("Test").Parent(1).Descendant().Class("SwitchCompat").Invoke("setChecked", Test));
                app.Screenshot("'Test' set to: " + Test);

                app.Query(x => x.Marked("Monitor").Parent(1).Descendant().Class("SwitchCompat").Invoke("setChecked", Monitor));
                app.Screenshot("'Monitor' set to: " + Monitor);
            }

            return this;
        }

        public void CloseSessionFilter()
        {
            app.Tap(DoneButton);
        }
       
    }
}


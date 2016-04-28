
using Xamarin.Forms;

namespace XamarinEvolve.Clients.UI
{
    public class EvolveNavigationPage : NavigationPage
    {
        public EvolveNavigationPage(Page root) : base(root)
        {
            Init();
            Title = root.Title;
            Icon = root.Icon;
        }

        public EvolveNavigationPage()
        {
            Init();
        }

        void Init()
        {
            if (Device.OS == TargetPlatform.iOS)
            {
                BarBackgroundColor = Color.FromHex("FAFAFA");
            }
            else
            {   
                BarBackgroundColor = (Color)Application.Current.Resources["Primary"];
                BarTextColor = (Color)Application.Current.Resources["NavigationText"];
            }
        }
    }
}


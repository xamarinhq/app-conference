using Xamarin.Forms;
using XamarinEvolve.DataObjects;
using XamarinEvolve.Clients.Portable;
using System.Windows.Input;

namespace XamarinEvolve.Clients.UI
{

    public class SessionCell: ViewCell
    {
        readonly INavigation navigation;
        public SessionCell (INavigation navigation = null)
        {
            Height = 120;
            View = new SessionCellView ();
            this.navigation = navigation;

        }

        protected override async void OnTapped()
        {
            base.OnTapped();
            if (navigation == null)
                return;
            var session = BindingContext as Session;
            if (session == null)
                return;
            
            App.Logger.TrackPage(AppPage.Session.ToString(), session.Title);
            await NavigationService.PushAsync(navigation, new SessionDetailsPage(session));
        }
    }

    public partial class SessionCellView : ContentView
    {
        public SessionCellView()
        {
            InitializeComponent();
            if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                LabelBadgetName.FontSize = 10;
        }


        public static readonly BindableProperty FavoriteCommandProperty = 
            BindableProperty.Create(nameof(FavoriteCommand), typeof(ICommand), typeof(SessionCellView), default(ICommand));

        public ICommand FavoriteCommand
        {
            get { return GetValue(FavoriteCommandProperty) as Command; }
            set { SetValue(FavoriteCommandProperty, value); }
        }
    }
}


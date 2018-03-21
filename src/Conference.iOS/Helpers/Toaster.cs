using Xamarin.Forms;
using Conference.Clients.Portable;
using Conference.iOS;
using UIKit;

[assembly:Dependency(typeof(Toaster))]
namespace Conference.iOS
{
    public class Toaster : IToast
    {
        public void SendToast(string message)
        {
            Device.BeginInvokeOnMainThread(() =>
                {
                    new UIAlertView(string.Empty, message, null, "OK").Show();
                });
        }
    }
}

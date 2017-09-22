using Xamarin.Forms;
using XamarinEvolve.Clients.Portable;
using XamarinEvolve.iOS;
using UIKit;

[assembly:Dependency(typeof(Toaster))]
namespace XamarinEvolve.iOS
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

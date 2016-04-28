using Windows.UI.Popups;
using Xamarin.Forms;
using XamarinEvolve.Clients.Portable;
using XamarinEvolve.UWP;

[assembly: Dependency(typeof(Toaster))]
namespace XamarinEvolve.UWP
{
    public class Toaster : IToast
    {
        public void SendToast(string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var dialog = new MessageDialog(message);
                dialog.ShowAsync();
            });
        }
    }
}

using Windows.UI.Popups;
using Xamarin.Forms;
using Conference.Clients.Portable;
using Conference.UWP;

[assembly: Dependency(typeof(Toaster))]
namespace Conference.UWP
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

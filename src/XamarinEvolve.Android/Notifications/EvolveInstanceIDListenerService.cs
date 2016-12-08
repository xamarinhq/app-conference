using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Gcm.Iid;

namespace XamarinEvolve.Droid.Notifications
{
    [Service(Exported =false)]
    [IntentFilter(new[] { InstanceID.IntentFilterAction})]
    public class EvolveInstanceIDListenerService : InstanceIDListenerService
    {
        public override void OnTokenRefresh()
        {
            EvolveRegistrationService.Register(this);
        }
    }
}
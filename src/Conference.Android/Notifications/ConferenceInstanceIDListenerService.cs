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
using Android.Gms.Iid;

namespace Conference.Droid.Notifications
{
    [Service(Exported =false)]
    [IntentFilter(new[] { InstanceID.IntentFilterAction})]
    public class ConferenceInstanceIDListenerService : InstanceIDListenerService
    {
        public override void OnTokenRefresh()
        {
            ConferenceRegistrationService.Register(this);
        }
    }
}
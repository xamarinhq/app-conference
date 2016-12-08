/*
using Android.App;
using Android.Content;
using Gcm;
using Gcm.Client;
using XamarinEvolve.Clients.Portable;

namespace XamarinEvolve.Droid
{
    [BroadcastReceiver(Permission=Constants.PERMISSION_GCM_INTENTS, Name="com.sample.evolve.GcmBroadcastReceiver")]
    [IntentFilter(new[] { Intent.ActionBootCompleted })] // Allow GCM on boot and when app is closed   
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]
    public class GcmBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
        
        public static string[] SENDERIDS = { ApiKeys.GoogleSenderId };

    }
}

*/
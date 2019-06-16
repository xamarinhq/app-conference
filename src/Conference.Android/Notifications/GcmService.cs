/*
using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Gcm;
using WindowsAzure.Messaging;
using Conference.Clients.Portable;
using Android.Support.V4.App;
using Gcm.Client;

namespace Conference.Droid
{



    [Service(Name="com.lacomarca.app.GcmService")] //Must use the service tag
    public class GcmService : GcmServiceBase
    {
        static NotificationHub hub;

        public static void Initialize(Context context)
        {
            try
            {
                if (ApiKeys.AzureServiceBusUrl == nameof(ApiKeys.AzureServiceBusUrl))
                    return;
                
                // Call this from our main activity
                var cs = ConnectionString.CreateUsingSharedAccessKeyWithListenAccess (
                    new Java.Net.URI (ApiKeys.AzureServiceBusUrl),
                    ApiKeys.AzureKey);

                hub = new NotificationHub (ApiKeys.AzureHubName, cs, context);
            }
            catch(Exception ex)
            {
                ex.Data["method"] = "GcmService.Initialize();";

            }
        }

        public static void Register(Context context)
        {
            try
            {
                if (ApiKeys.AzureServiceBusUrl == nameof(ApiKeys.AzureServiceBusUrl))
                    return;
                
                // Makes this easier to call from our Activity
                GcmClient.Register (context, GcmBroadcastReceiver.SENDERIDS);
            }
            catch(Exception ex)
            {
                ex.Data["method"] = "GcmService.Register();";

                Console.WriteLine("Unable to register GCMClient" + ex);
            }
        }

        public GcmService() : base(GcmBroadcastReceiver.SENDERIDS)
        {
        }

        protected override void OnRegistered (Context context, string registrationId)
        {
            //Receive registration Id for sending GCM Push Notifications to
            try
            {
                if (hub != null)
                    hub.Register (registrationId);

                Settings.Current.PushRegistered = true;
            }
            catch(Exception ex)
            {
                ex.Data["method"] = "GcmService.OnRegistered();";

                Console.WriteLine("Unable to register Hub" + ex);
            }
        }

        protected override void OnUnRegistered (Context context, string registrationId)
        {
            try
            {
                if (hub != null)
                    hub.Unregister ();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to unregister" + ex);
            }

        }



        protected override void OnMessage (Context context, Intent intent)
        {
            Console.WriteLine ("Received Notification");

            try
            {
                //Push Notification arrived - print out the keys/values
                if (intent != null || intent.Extras != null) 
                {

                    var keyset = intent.Extras.KeySet ();

                    foreach (var key in keyset)
                    {
                        var message = intent.Extras.GetString(key);
                        Console.WriteLine("Key: {0}, Value: {1}", key, message);
                        if(key == "message")
                            SendNotification(message);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine ("Error parsing message: " + ex);
            }

        }

        void SendNotification (string message)
        {
            try
            {
                Console.WriteLine ("SendNotification");
                var notificationManager = NotificationManagerCompat.From (this);

                Console.WriteLine ("Created Manager");
                var notificationIntent = new Intent(this, typeof(MainActivity));
                notificationIntent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask); 
                var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);

                Console.WriteLine ("Created Pending Intent");
                //var wearableExtender =
                //    new NotificationCompat.WearableExtender()
                //        .SetBackground(BitmapFactory.DecodeResource(Resources, Resource.Drawable.ic_background_conference));

                var style = new NotificationCompat.BigTextStyle();
                style.BigText(message);

                var builder = new NotificationCompat.Builder(this)
                    .SetContentIntent(pendingIntent)
                    .SetContentTitle("Conference")
                    .SetAutoCancel(true)
                    .SetStyle(style)
                    .SetSmallIcon(Resource.Drawable.ic_notification)
                    .SetContentText(message);
                //.Extend(wearableExtender);

                // Obtain a reference to the NotificationManager
                var id = Conference.Droid.Helpers.Settings.GetUniqueNotificationId();
                Console.WriteLine ("Got Unique ID: " + id);
                var notif = builder.Build ();
                notif.Defaults = NotificationDefaults.All;
                Console.WriteLine ("Notify");
                notificationManager.Notify(id, notif);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected override bool OnRecoverableError (Context context, string errorId)
        {
            //Some recoverable error happened
            return true;
        }

        protected override void OnError (Context context, string errorId)
        {
            //Some more serious error happened
        }
    }
}

*/
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
using Android.Gms.Gcm;
using Android.Support.V4.App;

namespace Conference.Droid.Notifications
{
    [Service(Exported =false)]
    [IntentFilter(new[] { GoogleCloudMessaging.IntentFilterActionReceive})]
    public class ConferenceGcmListenerService : GcmListenerService
    {
        const string TAG = "Conference16";
        public override void OnDeletedMessages()
        {
            base.OnDeletedMessages();
            Android.Util.Log.Debug(TAG, "Message Deleted");
        }

        public override void OnMessageReceived(string from, Bundle data)
        {
            Android.Util.Log.Debug(TAG, $"Message Received from {from}");

            try
            {    
                //Push Notification arrived - print out the keys/values and send notification
                if (data != null)
                {
                    var keyset = data.KeySet();

                    foreach (var key in keyset)
                    {
                        var message = data.GetString(key);
                        Android.Util.Log.Debug(TAG, $"Key: {key}, Value: {message}");
                        if (key == "message")
                            SendNotification(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error parsing message: " + ex);
            }

        }

        void SendNotification(string message)
        {
            try
            {
                Console.WriteLine("SendNotification");
                var notificationManager = NotificationManagerCompat.From(this);

                Console.WriteLine("Created Manager");
                var notificationIntent = new Intent(this, typeof(MainActivity));
                notificationIntent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);

                Console.WriteLine("Created Pending Intent");
                /*var wearableExtender =
                    new NotificationCompat.WearableExtender()
                        .SetBackground(BitmapFactory.DecodeResource(Resources, Resource.Drawable.ic_background_conference));*/

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
                Console.WriteLine("Got Unique ID: " + id);
                var notif = builder.Build();
                notif.Defaults = NotificationDefaults.All;
                Console.WriteLine("Notify");
                notificationManager.Notify(id, notif);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public override void OnMessageSent(string msgId)
        {
            base.OnMessageSent(msgId);
            Android.Util.Log.Debug(TAG, $"Message Sent: {msgId}");
        }

        public override void OnSendError(string msgId, string error)
        {
            base.OnSendError(msgId, error);
            Android.Util.Log.Debug(TAG, $"Message Filed {msgId}-{error}");
        }
    }
}
using System;

using Android.App;
using Android.Content;
using Android.Widget;
using Android.Util;
using Android.OS;
using System.Threading.Tasks;
using System.Threading;

using Android.Gms.Gcm;

using Xamarin.Forms;
using XamarinEvolve.Clients;
using XamarinEvolve.Clients.Portable;
using XamarinEvolve.DataStore.Abstractions;

namespace XamarinEvolve.Droid
{
    [Service (Name="com.sample.evolve.DataRefreshService", Exported = true, Permission = "com.google.android.gms.permission.BIND_NETWORK_TASK_SERVICE")]
    [IntentFilter (new [] { "com.google.android.gms.gcm.ACTION_TASK_READY" })]
    public class DataRefreshService : GcmTaskService
    {
        IBinder binder;

        const string LOG_TAG = "OnRunTask";

        public DataRefreshService ()
        {
            Log.Debug (LOG_TAG, "Service constructed");
        }

        public override IBinder OnBind (Intent intent)
        {
            binder = new DataRefreshServiceBinder (this);

            return binder;
        }

        public override void OnInitializeTasks ()
        {
            base.OnInitializeTasks ();
            ScheduleRefresh (this);
        }

        public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
        {
            return base.OnStartCommand (intent, flags, startId);
        }

        // Logic for task execution
        public override int OnRunTask (TaskParams @params)
        {
            Log.Debug (LOG_TAG, "Starting");
            try
            {
                System.Threading.Tasks.Task.Run (async () =>
                {
                    try
                    {
                        ViewModelBase.Init ();
                        // Download data
                        var manager = DependencyService.Get<IStoreManager> ();
                        if (manager == null)
                            return;
                        
                        await manager.SyncAllAsync (Settings.Current.IsLoggedIn);
                        Android.Util.Log.Debug (LOG_TAG, "Succeeded");
                        Settings.Current.LastSync = DateTime.UtcNow;
                        Settings.Current.HasSyncedData = true;
                    }
                    catch (Exception ex)
                    {
                        Android.Util.Log.Debug (LOG_TAG, ex.Message);
                     
                    }
                }).Wait(TimeSpan.FromSeconds(60));
            }
            catch
            {

               
            }

            Log.Debug (LOG_TAG, "Ending");

            return GcmNetworkManager.ResultSuccess;
        }

        public static void ScheduleRefresh (Context context)
        {
            try
            {
                Android.Util.Log.Debug ("app", "Start BackgroundDataRefreshService");
                var pt = new PeriodicTask.Builder ()
                    .SetPeriod (5400) // in seconds; 90 minutes
                    //.SetPeriod (180) // in seconds; 90 minutes
                    .SetFlex (600) // could be 10 mins before or after, that is cool
                    .SetService (Java.Lang.Class.FromType (typeof (DataRefreshService)))
                    .SetRequiredNetwork (Android.Gms.Gcm.Task.NetworkStateConnected)
                    .SetTag ("com.sample.evolve.backgrounddatarefresh")
                    .SetPersisted (true)
                    .SetRequiresCharging (false)
                    .SetUpdateCurrent (true)
                    .Build ();

                GcmNetworkManager.GetInstance (context).Schedule (pt);
            }
            catch
            {
            }
        }
    }


}
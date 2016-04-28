using System;
using XamarinEvolve.Portable.Services;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using XamarinEvolve.Portable.Interfaces;


[assembly: Dependency(typeof(AzureDataStore))]
namespace XamarinEvolve.Portable.Services
{
    public class AzureDataStore : IDataStore
    {
        public MobileServiceClient MobileService { get; set; }

        //IMobileServiceSyncTable<Monkey> monkeyTable;
        bool initialized = false;

        public AzureDataStore()
        {
            MobileService = new MobileServiceClient(
                "https://xamarinevolve.azurewebsites.net");
        }

        #region IDataStore implementation

        public async Task InitializeStore()
        {
            initialized = true;
            const string path = "syncstore.db";
            var store = new MobileServiceSQLiteStore(path);
            //store.DefineTable<Monkey>();
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //monkeyTable = MobileService.GetSyncTable<Monkey>();
        }

        public XamarinEvolve.Portable.Model.Individual GetSpeaker(string name)
        {
            throw new NotImplementedException();
        }

        public Task<XamarinEvolve.Portable.Model.Session> GetSessionAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<System.Collections.Generic.IEnumerable<XamarinEvolve.Portable.Model.Session>> GetSessionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<System.Collections.Generic.IEnumerable<XamarinEvolve.Portable.Model.Session>> GetSpeakerSessionsAsync(string speakerName)
        {
            throw new NotImplementedException();
        }

        public Task<System.Collections.Generic.IEnumerable<XamarinEvolve.Portable.Model.Individual>> GetSpeakersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<System.Collections.Generic.IEnumerable<XamarinEvolve.Portable.Model.Individual>> GetSponsorsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<System.Collections.Generic.IEnumerable<XamarinEvolve.Portable.Model.Individual>> GetExhibitorsAsync()
        {
            throw new NotImplementedException();
        }

        #endregion

        /*public async Task<IEnumerable<Monkey>> GetMonkeysAsync()
        {
            if (!initialized)
                await InitializeStore();

            await monkeyTable.PullAsync("allMonkeys", monkeyTable.CreateQuery());

            return await monkeyTable.OrderBy(x=>x.Name).ToEnumerableAsync();
        }*/
    }
}

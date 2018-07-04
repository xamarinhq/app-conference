using System;
using Conference.DataStore.Abstractions;
using Conference.DataObjects;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using System.Diagnostics;
using Xamarin.Essentials;

namespace Conference.DataStore.Azure
{
    public class BaseStore<T> : IBaseStore<T> where T : class, IBaseDataObject, new()
    {
        IStoreManager storeManager;

        public virtual string Identifier => "Items";

        IMobileServiceSyncTable<T> table;
        protected IMobileServiceSyncTable<T> Table
        {
            get { return table ?? (table = StoreManager.MobileService.GetSyncTable<T>()); }
          
        }

        public void DropTable()
        {
            table = null;
        }

        public BaseStore()
        {
            
        }

        #region IBaseStore implementation

        public async Task InitializeStore()
        {
            if (storeManager == null)
                storeManager = DependencyService.Get<IStoreManager>();

            if (!storeManager.IsInitialized)
                await storeManager.InitializeAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeStore().ConfigureAwait (false);
            if(forceRefresh)
                await PullLatestAsync().ConfigureAwait (false);
            
            return await Table.ToEnumerableAsync().ConfigureAwait(false);
        }

        public virtual async Task<T> GetItemAsync(string id)
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);
            var items = await Table.Where(s => s.Id == id).ToListAsync().ConfigureAwait(false);

            if (items == null || items.Count == 0)
                return null;
            
            return items[0];
        }

        public virtual async Task<bool> InsertAsync(T item)
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait (false);
            await Table.InsertAsync(item).ConfigureAwait(false);
            await SyncAsync().ConfigureAwait (false);
            return true;
        }

        public virtual async Task<bool> UpdateAsync(T item)
        {
            await InitializeStore().ConfigureAwait(false);
            await Table.UpdateAsync(item).ConfigureAwait(false);
            await SyncAsync().ConfigureAwait (false);
            return true;
        }

        public virtual async Task<bool> RemoveAsync(T item)
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync ().ConfigureAwait (false);
            await Table.DeleteAsync(item).ConfigureAwait(false);
            await SyncAsync().ConfigureAwait (false);
            return true;
        }

        public async Task<bool> PullLatestAsync()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                Debug.WriteLine("Unable to pull items, we are offline");
                return false;
            }
            try
            {
                await Table.PullAsync($"all{Identifier}", Table.CreateQuery()).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to pull items, that is alright as we have offline capabilities: " + ex);
                return false;
            }
            return true;
        }


        public async Task<bool> SyncAsync()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                Debug.WriteLine("Unable to sync items, we are offline");
                return false;
            }
            try
            {
                await StoreManager.MobileService.SyncContext.PushAsync().ConfigureAwait(false);
                if(!(await PullLatestAsync().ConfigureAwait(false)))
                    return false;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Unable to sync items, that is alright as we have offline capabilities: " + ex);
                return false;
            }
            finally
            {
            }
            return true;
        }

        #endregion
    }
}


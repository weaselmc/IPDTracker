using System.Collections.Generic;
using System.Threading.Tasks;

using IPDTracker.Models;

[assembly: Xamarin.Forms.Dependency(typeof(IPDTracker.Services.DbEntryStore))]
namespace IPDTracker.Services
{
   
    class DbEntryStore : IDataStore<BillingEntry>
    {

        public DbEntryStore()
        {
           DataStore.DbConn.CreateTableAsync<BillingEntry>();
        }
        public async Task<int> AddItemAsync(BillingEntry item)
        {
            var result = await DataStore.DbConn.InsertAsync(item);
            return await Task.FromResult(result);
        }

        public async Task<int> UpdateItemAsync(BillingEntry item)
        {
            var result = await DataStore.DbConn.UpdateAsync(item);
            return await Task.FromResult(result);
        }

        public async Task<int> DeleteItemAsync(BillingEntry item)
        {
            var result = await DataStore.DbConn.DeleteAsync(item);
            return await Task.FromResult(result);
        }
        public async Task<BillingEntry> GetItemAsync(string id)
        {
            return await Task.FromResult(
                await DataStore.DbConn.FindAsync<BillingEntry>(id));
        }

        public async Task<IEnumerable<BillingEntry>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(
                await DataStore.DbConn.Table<BillingEntry>().ToListAsync());
        }
    }
}

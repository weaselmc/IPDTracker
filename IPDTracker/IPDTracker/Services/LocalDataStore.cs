using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using IPDTracker.Models;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(IPDTracker.Services.LocalDataStore))]
namespace IPDTracker.Services
{
   
    public class LocalDataStore : IDataStore<BillingEntry>
    {
        static string dbname = "ipd.db.sqlite";
        static LocalDataStore defaultStore = new LocalDataStore();
#if __ANDROID__
        public static string path = Environment.GetFolderPath(
        Environment.SpecialFolder.Personal);
#else
#if __IOS__
        public static string path = Path.Combine(Environment.GetFolderPath(
        Environment.SpecialFolder.Personal), "..", "Library");
#else
        //UWP
        public static string path = Windows.Storage.ApplicationData.
            Current.LocalFolder.Path;
#endif
#endif
        public static string DbPath = Path.Combine(path, dbname);
        public static SQLiteAsyncConnection DbConn =
            new SQLiteAsyncConnection(DbPath);

        

        public LocalDataStore()
        {
           DbConn.CreateTableAsync<BillingEntry>();
        }
        public async Task<int> AddItemAsync(BillingEntry item)
        {
            item.DateModified = DateTime.Now;
            var result = await DbConn.InsertAsync(item);
            return await Task.FromResult(result);
        }

        public async Task<int> UpdateItemAsync(BillingEntry item)
        {
            item.DateModified = DateTime.Now;
            var result = await DbConn.UpdateAsync(item);
            return await Task.FromResult(result);
        }

        public async Task<int> DeleteItemAsync(string id)
        {
            var result = await DbConn.DeleteAsync(await GetItemAsync(id));
            return await Task.FromResult(result);
        }
        public async Task<BillingEntry> GetItemAsync(string id)
        {
            return await Task.FromResult(
                await DbConn.FindAsync<BillingEntry>(id));
        }

        public async Task<IEnumerable<BillingEntry>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(
                await DbConn.Table<BillingEntry>().ToListAsync());
        }
    }
}

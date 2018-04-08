
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IPDTracker.Models;

[assembly: Xamarin.Forms.Dependency(typeof(IPDTracker.Services.DbItemStore))]
namespace IPDTracker.Services
{
    class DbItemStore: IDataStore<Item>
    {
        public DbItemStore()
        {
            DataStore.DbConn.CreateTableAsync<Item>();
        }
        public async Task<int> AddItemAsync(Item item)
        {
            var result = await DataStore.DbConn.InsertAsync(item);
            return await Task.FromResult(result);
        }

        public async Task<int> UpdateItemAsync(Item item)
        {
            var result = await DataStore.DbConn.UpdateAsync(item);
            return await Task.FromResult(result);
        }

        public async Task<int> DeleteItemAsync(Item item)
        {
            var result = await DataStore.DbConn.DeleteAsync(item);
            return await Task.FromResult(result);
        }
        public async Task<Item> GetItemAsync(string id)
        {            
            return await Task.FromResult(
                await DataStore.DbConn.FindAsync<Item>(id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(
                await DataStore.DbConn.Table<Item>().ToListAsync());
        }       

    }

}

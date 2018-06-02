using IPDTracker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IPDTracker.Services
{
    public static class SyncService
    {
        public static IDataStore<BillingEntry> LocalDataStore =>
            DependencyService.Get<IDataStore<BillingEntry>>() ?? new LocalDataStore();
        public static AzureDataStore AzureDataStore = DependencyService.Get<AzureDataStore>();

        public static async Task SyncAsync()
        {
            var items = await LocalDataStore.GetItemsAsync(true);
            if(items.Count<BillingEntry>() != 0)
            {
                foreach(var item in items)
                {
                    item.DateModified = DateTime.Now;
                    try
                    {
                        await AzureDataStore.AddItemAsync(item);
                        await LocalDataStore.DeleteItemAsync(item.Id.ToString());
                    }
                    catch
                    {
                        Debug.WriteLine(item.Id.ToString() + " failed to sync.");
                    }
                }
            }

            await Task.CompletedTask;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using IPDTracker.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.IO;
using System.Diagnostics;
using Xamarin.Forms;
using System.Linq;
using System.Net.Http.Headers;

[assembly: Xamarin.Forms.Dependency(typeof(IPDTracker.Services.AzureDataStore))]
namespace IPDTracker.Services
{
	public partial class AzureDataStore : IDataStore<BillingEntry>
	{        
        static AzureDataStore defaultInstance = new AzureDataStore();
        MobileServiceClient msclient;
        
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
        public static string dbname = @"localstore.db";
        public static string DbPath = Path.Combine(path, dbname);
        HttpClient client;
        IEnumerable<BillingEntry> items;
        public static IDataStore<BillingEntry> LocalDataStore =>
           DependencyService.Get<IDataStore<BillingEntry>>() ?? new LocalDataStore();

        public AzureDataStore()
		{
            client = new HttpClient();
            client.BaseAddress = new Uri($"{IPDTracker.App.AzureBackendUrl}/");

            //client.DefaultRequestHeaders.Add("Authorization", IPDTracker.App.User.MobileServiceAuthenticationToken);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", IPDTracker.App.User.MobileServiceAuthenticationToken);
            msclient = new MobileServiceClient(IPDTracker.App.AzureBackendUrl);
            
            //var store = new MobileServiceSQLiteStore(offlineDbPath);
            //store.DefineTable<BillingEntry>();

            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            //msclient.SyncContext.InitializeAsync(store);
            //BillingEntriesTable = msclient.GetSyncTable<BillingEntry>();
            items = new List<BillingEntry>();
		}
        public static AzureDataStore DefaultStore
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return msclient; }
        }

        public async Task<IEnumerable<BillingEntry>> GetItemsAsync(bool forceRefresh = false) //&& msclient.CurrentUser != null ??
        {
			if (forceRefresh && CrossConnectivity.Current.IsConnected)
			{
                //get only items from msclient.CurrentUser.UserId? Need to mod Controller :(
                var json = await client.GetStringAsync($"api/billingentries");
				items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<BillingEntry>>(json));
			}

			return items;
		}

		public async Task<BillingEntry> GetItemAsync(string id)
		{
			if (id != null && CrossConnectivity.Current.IsConnected) //&& msclient.CurrentUser != null ??
            {                
                var json = await client.GetStringAsync($"api/billingentries/{id}");
				return await Task.Run(() => JsonConvert.DeserializeObject<BillingEntry>(json));
			}

			return null;
		}

		public async Task<int> AddItemAsync(BillingEntry item)
		{
			if (item == null || !CrossConnectivity.Current.IsConnected)
				return 404;
            item.DateModified = DateTime.Now;
            //item.UserId = msclient.CurrentUser.UserId;
			var serializedItem = JsonConvert.SerializeObject(item);

			var response = await client.PostAsync($"api/billingentries", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

			return (int)response.StatusCode;
		}

		public async Task<int> UpdateItemAsync(BillingEntry item)
		{
			if (item == null || item.Id == null || !CrossConnectivity.Current.IsConnected)
				return 404; //should store to localdb cache and sync later baseed on datemodified
            item.DateModified = DateTime.Now;
            //item.UserId = msclient.CurrentUser.UserId;
            var serializedItem = JsonConvert.SerializeObject(item);
			var buffer = Encoding.UTF8.GetBytes(serializedItem);
			var byteContent = new ByteArrayContent(buffer);

			var response = await client.PutAsync(new Uri($"api/billingentries/{item.Id}"), byteContent);

			return (int)response.StatusCode;
		}

		public async Task<int> DeleteItemAsync(string id)
		{
			if (string.IsNullOrEmpty(id) && !CrossConnectivity.Current.IsConnected)
				return 404;

			var response = await client.DeleteAsync($"api/billingentries/{id}");

            return (int)response.StatusCode;

        }
        public async Task SyncAsync()
        {
            var items = await LocalDataStore.GetItemsAsync(true);
            if (items.Count<BillingEntry>() != 0)
            {
                foreach (var item in items)
                {
                    item.DateModified = DateTime.Now;
                    try
                    {
                        await AzureDataStore.DefaultStore.AddItemAsync(item);
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
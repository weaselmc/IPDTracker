using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using IPDTracker.Models;

[assembly: Xamarin.Forms.Dependency(typeof(IPDTracker.Services.AzureDataStore))]
namespace IPDTracker.Services
{
	public class AzureDataStore : IDataStore<BillingEntry>
	{
		HttpClient client;
		IEnumerable<BillingEntry> items;

		public AzureDataStore()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

			items = new List<BillingEntry>();
		}

		public async Task<IEnumerable<BillingEntry>> GetItemsAsync(bool forceRefresh = false)
		{
			if (forceRefresh && CrossConnectivity.Current.IsConnected)
			{
				var json = await client.GetStringAsync($"api/billingentries");
				items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<BillingEntry>>(json));
			}

			return items;
		}

		public async Task<BillingEntry> GetItemAsync(string id)
		{
			if (id != null && CrossConnectivity.Current.IsConnected)
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
			var serializedItem = JsonConvert.SerializeObject(item);

			var response = await client.PostAsync($"api/billingentries", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

			return (int)response.StatusCode;
		}

		public async Task<int> UpdateItemAsync(BillingEntry item)
		{
			if (item == null || item.Id == null || !CrossConnectivity.Current.IsConnected)
				return 404; //should store to localdb cache and sync later baseed on datemodified
            item.DateModified = DateTime.Now;
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
	}
}
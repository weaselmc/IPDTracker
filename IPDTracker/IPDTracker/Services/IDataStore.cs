using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPDTracker.Services
{
    public interface IDataStore<T>
    {

        Task<int> AddItemAsync(T item);
        Task<int> UpdateItemAsync(T item);
        Task<int> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}

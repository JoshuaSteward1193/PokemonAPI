using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PokeAPI.Services
{
    public interface IReadOnlyDataStore<T>
    {
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}

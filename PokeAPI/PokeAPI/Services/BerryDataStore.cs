using Newtonsoft.Json;
using PokeAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PokeAPI.Services
{
    class BerryDataStore : IReadOnlyDataStore<Berry>
    {
        HttpClient client;
        public List<Berry> BerryList { get; private set; }
        public BerryDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.PokemonBaseUrl}");
            BerryList = new List<Berry>();
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<Berry> GetItemAsync(string id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"berry/{id}");
                Debug.WriteLine(json);
                return await Task.Run(() => JsonConvert.DeserializeObject<Berry>(json));
            }
            return null;
        }

        public async Task<IEnumerable<Berry>> GetItemsAsync(bool forceRefresh = false)
        {
            RootObject berryList = new RootObject();
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"berry");
                var response = await Task.Run(() => JsonConvert.DeserializeObject<RootObject>(json));

                berryList = response;
            }
            List<Berry> returnList = new List<Berry>();
            foreach (Result r in berryList.results)
            {
                Berry temp = new Berry();
                var json = await client.GetStringAsync($"berry/{r.name}");
                temp = await Task.Run(() => JsonConvert.DeserializeObject<Berry>(json));
                BerryList.Add(temp);
            }
            return BerryList;
        }
    }
}

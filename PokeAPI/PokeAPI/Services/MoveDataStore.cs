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
    class MoveDataStore : IReadOnlyDataStore<Move>
    {
        HttpClient client;
        public List<Move> MoveList { get; private set; }
        public MoveDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.PokemonBaseUrl}");
            MoveList = new List<Move>();
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<Move> GetItemAsync(string id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"move/{id}");
                Debug.WriteLine(json);
                return await Task.Run(() => JsonConvert.DeserializeObject<Move>(json));
            }
            return null;
        }

        public async Task<IEnumerable<Move>> GetItemsAsync(bool forceRefresh = false)
        {
            RootObject moveList = new RootObject();
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"move");
                var response = await Task.Run(() => JsonConvert.DeserializeObject<RootObject>(json));

                moveList = response;
            }
            List<Move> returnList = new List<Move>();
            foreach (Result r in moveList.results)
            {
                Move temp = new Move();
                var json = await client.GetStringAsync($"move/{r.name}");
                try
                {
                    temp = await Task.Run(() => JsonConvert.DeserializeObject<Move>(json));
                }
                finally { }
                
                MoveList.Add(temp);
            }
            return MoveList;
        }
    }
}

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
    public class PokemonDataStore : IReadOnlyDataStore<Pokemon>
    {
        HttpClient client;
        public List<Pokemon> pokemonList { get; private set; }
        public PokemonDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.PokemonBaseUrl}");
            pokemonList = new List<Pokemon>();
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<Pokemon> GetItemAsync(string id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"pokemon/{id}");
                Debug.WriteLine(json);
                return await Task.Run(() => JsonConvert.DeserializeObject<Pokemon>(json));
            }
            return null;
        }

        public async Task<IEnumerable<Pokemon>> GetItemsAsync(bool forceRefresh = false)
        {
            RootObject pokeList = new RootObject();
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"pokemon");
                var response = await Task.Run(() => JsonConvert.DeserializeObject<RootObject>(json));

                pokeList = response;
            }
            List<Pokemon> returnList = new List<Pokemon>();
            foreach (Result r in pokeList.results)
            {
                Pokemon temp = new Pokemon();
                var json = await client.GetStringAsync($"pokemon/{r.name}");
                temp = await Task.Run(() => JsonConvert.DeserializeObject<Pokemon>(json));
                pokemonList.Add(temp);
            }
            return pokemonList;
        }
    }
}

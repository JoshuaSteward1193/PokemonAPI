using PokeAPI.Models;
using PokeAPI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PokeAPI.ViewModels
{
    public class PokemonViewModel : BaseViewModel
    {
        public ObservableCollection<Pokemon> Pokemon { get; set; }
        public Command LoadItemsCommand { get; set; }
        public PokemonDataStore DataStore = new PokemonDataStore();
        public PokemonViewModel()
        {
            Title = "Pokémon";
            Pokemon = new ObservableCollection<Pokemon>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                Pokemon.Clear();
                var pokemon = await DataStore.GetItemsAsync(true);
                foreach(var poke in pokemon)
                {
                    Pokemon temp = poke;
                    temp.name = UppercaseFirst(temp.name);
                    Pokemon.Add(temp);
                }
            } 
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}

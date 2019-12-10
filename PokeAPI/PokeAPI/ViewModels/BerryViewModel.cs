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
    class BerryViewModel : BaseViewModel
    {
        public ObservableCollection<Berry> Berries { get; set; }
        public Command LoadItemsCommand { get; set; }
        public BerryDataStore DataStore = new BerryDataStore();
        public BerryViewModel()
        {
            Title = "Berries";
            Berries = new ObservableCollection<Berry>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                Berries.Clear();
                var berry = await DataStore.GetItemsAsync(true);
                foreach (var b in berry)
                {
                    Berry temp = b;
                    temp.name = UppercaseFirst(temp.name);
                    Berries.Add(temp);
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

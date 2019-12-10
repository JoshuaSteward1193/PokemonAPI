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
    class MoveViewModel : BaseViewModel
    {
        public ObservableCollection<Move> Moves { get; set; }
        public Command LoadItemsCommand { get; set; }
        public MoveDataStore DataStore = new MoveDataStore();
        public MoveViewModel()
        {
            Title = "Moves";
            Moves = new ObservableCollection<Move>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
            {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                Moves.Clear();
                var move = await DataStore.GetItemsAsync(true);
                foreach (var m in move)
                {
                    Move temp = m;
                    temp.name = UppercaseFirst(temp.name);
                    Moves.Add(temp);
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

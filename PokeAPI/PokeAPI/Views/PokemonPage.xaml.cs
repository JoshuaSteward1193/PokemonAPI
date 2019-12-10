using PokeAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeAPI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonPage : ContentPage
    {
        PokemonViewModel viewModel;
        public PokemonPage()
        {
            InitializeComponent();
            viewModel = new PokemonViewModel();
            BindingContext = viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Pokemon.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
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
    public partial class MovePage : ContentPage
    {
        MoveViewModel viewModel;

        public MovePage()
        {
            InitializeComponent();
            viewModel = new MoveViewModel();
            BindingContext = viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Moves.Count == 0) viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
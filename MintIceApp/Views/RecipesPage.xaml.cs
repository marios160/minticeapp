using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MintIceApp.Models;
using MintIceApp.Views;
using MintIceApp.ViewModels;

namespace MintIceApp.Views
{
    public partial class RecipesPage : ContentPage
    {
        RecipesViewModel _viewModel;

        public RecipesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new RecipesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
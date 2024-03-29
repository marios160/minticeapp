﻿using System;
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
using MintIceApp.Repositories;

namespace MintIceApp.Views
{
    public partial class RecipesPage : ContentPage
    {
        RecipesViewModel _viewModel;

        public RecipesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new RecipesViewModel(this);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.Items.Clear();
            foreach (var item in RecipeRepository.FindByName(e.NewTextValue).Result)
            {
                _viewModel.Items.Add(item);
            }
        }
    }
}
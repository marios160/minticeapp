using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MintIceApp.Models;
using MintIceApp.ViewModels;
using MintIceApp.Repositories;
using System.Diagnostics;

namespace MintIceApp.Views
{
    public partial class NewRecipePage : ContentPage
    {
        private NewRecipeViewModel vm;

        public NewRecipePage()
        {
            InitializeComponent();
            this.vm = new NewRecipeViewModel();
            BindingContext = this.vm;
        }

        private void ingredients_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            add.IsVisible = false;
            save.IsVisible = true;
            cancel.IsVisible = true;
            Ingredient i = e.Item as Ingredient;
            vm.Ingredient = i;
            vm.IngredientName = i.Name;
            vm.IngredientQuantity = i.Quantity;
        }


        private void cancel_Clicked(object sender, EventArgs e)
        {
            add.IsVisible = true;
            save.IsVisible = false;
            cancel.IsVisible = false;
            vm.Ingredient = null;
            vm.IngredientName = "";
            vm.IngredientQuantity = 0;
        }

        private void save_Clicked(object sender, EventArgs e)
        {
            add.IsVisible = true;
            save.IsVisible = false;
            cancel.IsVisible = false;
            vm.EditIngredient();
        }
    }
}
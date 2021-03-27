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
            vm.Ingredients.RemoveAt(e.ItemIndex);
        }


        private void remove_Clicked(object sender, EventArgs e)
        {

        }

        private void remove_Tapped(object sender, EventArgs e)
        {

            int i = Convert.ToInt32((e as TappedEventArgs).Parameter);
            Debug.WriteLine(i);
            vm.Ingredients.RemoveAt(i);
        }
    }
}
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

        private void quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            Entry entry = (Entry)sender;
            if (e.NewTextValue.Length > 5)
            {
                string value = entry.Text;
                value = value.Remove(0, 2);
                value = value.Insert(1, ".");
                entry.Text = value;
            } 
            else if(e.NewTextValue.Length < 5)
            {
                string value = entry.Text;
                value = value.Remove(1, 1);
                value = "0" + value;
                value = value.Insert(1, ".");
                entry.Text = value;
            }
        }

        private void ingredients_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            vm.Ingredients.RemoveAt(e.ItemIndex);
        }


        private void remove_Clicked(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((e as ClickedEventArgs).Parameter);
            Debug.WriteLine(id);

        }

    }
}
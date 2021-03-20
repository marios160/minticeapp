using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MintIceApp.Models;
using MintIceApp.ViewModels;

namespace MintIceApp.Views
{
    public partial class NewRecipePage : ContentPage
    {
        public Recipe Item { get; set; }

        public NewRecipePage()
        {
            InitializeComponent();
            BindingContext = new NewRecipeViewModel();
        }
    }
}
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

        

    }
}
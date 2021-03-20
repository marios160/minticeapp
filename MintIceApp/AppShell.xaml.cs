using System;
using System.Collections.Generic;
using MintIceApp.Services;
using MintIceApp.ViewModels;
using MintIceApp.Views;
using Xamarin.Forms;

namespace MintIceApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            new DataBase();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewRecipePage), typeof(NewRecipePage));
        }

    }
}

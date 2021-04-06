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
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewRecipePage), typeof(NewRecipePage));
            Routing.RegisterRoute(nameof(SummaryPage), typeof(SummaryPage));
            Routing.RegisterRoute(nameof(MonthSumaryPage), typeof(MonthSumaryPage));
        }

    }
}

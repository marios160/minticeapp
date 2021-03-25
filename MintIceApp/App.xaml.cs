using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MintIceApp.Services;
using MintIceApp.Views;

namespace MintIceApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            DataBase db = new DataBase();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

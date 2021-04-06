using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MintIceApp.Services;
using MintIceApp.Views;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using System.IO;

namespace MintIceApp
{
    public partial class App : Xamarin.Forms.Application
    {

        public App()
        {
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            InitializeComponent();
            MainPage = new AppShell();
            DataBase db = new DataBase();
            if (!Directory.Exists("/storage/emulated/0/MintIceApp"))
            {
                Directory.CreateDirectory("/storage/emulated/0/MintIceApp");
                Directory.CreateDirectory("/storage/emulated/0/MintIceApp/Summaries");
                Directory.CreateDirectory("/storage/emulated/0/MintIceApp/Export");
            }

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

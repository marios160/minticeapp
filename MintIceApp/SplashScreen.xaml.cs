﻿
using MintIceApp.Services;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MintIceApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreen : ContentPage
    {
        public SplashScreen()
        { 
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            await Task.Delay(1);
            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (status == PermissionStatus.Denied)
            {
                status = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }
            status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (status == PermissionStatus.Denied)
            {
                status = await Permissions.RequestAsync<Permissions.StorageRead>();
            }
            
            DataBase db = new DataBase();
            if (!Directory.Exists("/storage/emulated/0/MintIceApp"))
            {
                Directory.CreateDirectory("/storage/emulated/0/MintIceApp");
                Directory.CreateDirectory("/storage/emulated/0/MintIceApp/Summaries");
                Directory.CreateDirectory("/storage/emulated/0/MintIceApp/Export");
            }
            App.Current.MainPage = new AppShell();
            base.OnAppearing();
        }
    }
}
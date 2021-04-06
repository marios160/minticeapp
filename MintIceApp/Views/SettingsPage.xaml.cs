using MintIceApp.Services;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MintIceApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void export_Clicked(object sender, EventArgs e)
        {
            var bytes = System.IO.File.ReadAllBytes(DataBase.DbPath);
            var filename = "minticeapp_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".db3";
            var fileCopyName = Path.Combine("/storage/emulated/0/MintIceApp/Export", filename);
            System.IO.File.WriteAllBytes(fileCopyName, bytes);
            CrossToastPopUp.Current.ShowToastMessage("Baza danych została wyeksportowana (Pamięć wewnętrzna/MintIceApp/Export/" + filename);

        }

        private async void import_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool ask = await DisplayAlert("Importowanie bazy danych", "Czy na pewno chcesz zaimportować nową bazę danych? Wszystkie bieżące dane zostaną usunięte!", "Tak", "Nie");
                if (ask)
                {
                    FileData fileData = await CrossFilePicker.Current.PickFile();
                    if (fileData == null)
                        return; // user canceled file 
                    string contents = System.Text.Encoding.UTF8.GetString(fileData.DataArray);
                    await DataBase.db.CloseAsync();
                    System.IO.File.WriteAllBytes(DataBase.DbPath, fileData.DataArray);
                    DataBase.db = new SQLite.SQLiteAsyncConnection(DataBase.DbPath);
                    CrossToastPopUp.Current.ShowToastMessage("Baza danych została zaimportowana");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", "Zrestartuj aplikację", "OK");
                Debug.WriteLine("Exception choosing file: " + ex.ToString());
            }
        }

        private async void clear_Clicked(object sender, EventArgs e)
        {
            bool ask = await DisplayAlert("Czyszczenie bazy danych", "Czy na pewno chcesz wyczyścić bazę danych? Wszystkie dane aplikacji zostaną usunięte!", "Tak", "Nie");
            if (ask)
            {
                DataBase.clearDB();
                CrossToastPopUp.Current.ShowToastMessage("Baza danych została wyczyszczona");
            }
        }
    }
}
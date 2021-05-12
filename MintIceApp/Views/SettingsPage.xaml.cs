using MintIceApp.Models;
using MintIceApp.Repositories;
using MintIceApp.Services;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
            string sql = "";
            List<Recipe> recipies = RecipeRepository.FindAll().Result;
            if (recipies.Count > 0)
            {
                sql += "INSERT INTO Recipe (Id,Name,Note,CreatedAt,UpdatedAt,Favourite) VALUES \n";
                foreach (var item in recipies)
                {
                    Console.WriteLine(item.CreatedAt);
                    sql += "(" + item.Id + ",'" + item.Name + "','" + item.Note + "'," + item.CreatedAt.ToBinary() + "," + item.UpdatedAt.ToBinary() + "," + item.Favourite + "),\n";
                }
                sql = sql.Remove(sql.Length - 2);
                sql += ";?\n";
            }
            sql += "-- ------------------------------\n";

            List<Product> products = ProductRepository.FindAll().Result;
            if (products.Count > 0)
            {

                sql += "INSERT INTO Product (Id,Name,Quantity,CreatedAt,UpdatedAt,RecipeId) VALUES \n";
                foreach (var item in products)
                {
                    sql += "(" + item.Id + ",'" + item.Name + "','" + item.Quantity.ToString().Replace(',', '.') + "'," + item.CreatedAt.ToBinary() + "," + item.UpdatedAt.ToBinary() + "," + item.RecipeId + "),\n";
                }
                sql = sql.Remove(sql.Length - 2);
                sql += ";?\n";
            }
            sql += "-- ------------------------------\n";

            List<Ingredient> ingredients = IngredientRepository.FindAll().Result;
            if (ingredients.Count > 0) 
            { 
                sql += "INSERT INTO Ingredient (Id,Name,Quantity,RecipeId) VALUES \n";
                foreach (var item in ingredients)
                {
                    sql += "(" + item.Id + ",'" + item.Name + "'," + item.Quantity.ToString().Replace(',','.') + "," + item.RecipeId + "),\n";
                }
                sql = sql.Remove(sql.Length - 2);
                sql += ";?\n\n";
            }

            var filename = "minticeapp_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".sql";
            var fileCopyName = Path.Combine("/storage/emulated/0/MintIceApp/Export", filename);
            System.IO.File.WriteAllText(fileCopyName, sql);
            CrossToastPopUp.Current.ShowToastMessage("Baza danych została wyeksportowana (Pamięć wewnętrzna/MintIceApp/Export/" + filename);

        }

        private async void import_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool ask = await DisplayAlert("Importowanie bazy danych", "Czy na pewno chcesz zaimportować nową bazę danych? Wszystkie bieżące dane zostaną usunięte!", "Tak", "Nie");
                if (ask)
                {
                    var result = await FilePicker.PickAsync();
                    string contents = "";
                    if (result != null)
                    {
                        if (result.FileName.EndsWith("sql", StringComparison.OrdinalIgnoreCase))
                        {
                            var stream = await result.OpenReadAsync();
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                contents = reader.ReadToEnd();
                            }
                        }
                    }

                    string[] separator = { "-- ------------------------------" };
                    string[] queries = contents.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    await DataBase.clearDB();
                    if (queries.Length > 0)
                        await DataBase.db.QueryAsync<Recipe>(queries[0], "");
                    if (queries.Length > 1)
                        await DataBase.db.QueryAsync<Product>(queries[1], "");
                    if (queries.Length > 2)
                        await DataBase.db.QueryAsync<Ingredient>(queries[2], "");


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
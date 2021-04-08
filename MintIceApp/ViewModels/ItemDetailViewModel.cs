using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using MintIceApp.Models;
using MintIceApp.Repositories;
using MintIceApp.Services;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using Plugin.Toast;
using Xamarin.Forms;

namespace MintIceApp.ViewModels
{
    [QueryProperty(nameof(RecipeId), nameof(RecipeId))]
    [QueryProperty(nameof(ProductId), nameof(ProductId))]

    public class ItemDetailViewModel : BaseViewModel
    {
        private string recipeName;
        private string recipeNote;
        private decimal sum;
        private ObservableCollection<Ingredient> ingredients;
        private string recipeId;
        private string productId;
        private Recipe recipe;
        private Product product;

        public Command SaveCommand { get; }
        public Command PrintCommand { get; }

        public ItemDetailViewModel()
        {
            Title = "Dodaj produkt";
            Ingredients = new ObservableCollection<Ingredient>();
            SaveCommand = new Command(OnSave);
            PrintCommand = new Command(OnPrint);
            this.PropertyChanged += (_, __) => PrintCommand.ChangeCanExecute();
        }

        public string RecipeId
        {
            get { return recipeId; }
            set
            {
                recipeId = value;
                Recipe = RecipeRepository.FindOneById(Convert.ToInt32(value)).Result;
                RecipeName = Recipe.Name;
                RecipeNote = Recipe.Note;
                foreach (var item in Recipe.GetIngredients())
                {
                    Ingredients.Add(item);
                }

            }
        }
        public string ProductId
        {
            get { return productId; }
            set
            {
                productId = value;
                Product = ProductRepository.FindOneById(Convert.ToInt32(value)).Result;
                Recipe = RecipeRepository.FindOneById(Product.RecipeId).Result;
                RecipeName = Recipe.Name;
                RecipeNote = Recipe.Note;
                Sum = Product.Quantity;
                foreach (var item in Recipe.GetIngredients())
                {
                    Ingredients.Add(item);
                }
                RefreshQuantities();
                Title = "Edytuj produkt";

            }
        }
        public Recipe Recipe
        {
            get => recipe;
            set => SetProperty(ref recipe, value);
        }
        public Product Product
        {
            get => product;
            set => SetProperty(ref product, value);
        }

        public decimal Sum
        {
            get => sum;
            set => SetProperty(ref sum, value);
        }

        public string RecipeName
        {
            get => recipeName;
            set => SetProperty(ref recipeName, value);
        }

        public string RecipeNote
        {
            get => recipeNote;
            set => SetProperty(ref recipeNote, value);
        }

        public ObservableCollection<Ingredient> Ingredients
        {
            get => ingredients;
            set => SetProperty(ref ingredients, value);
        }

        public void RefreshQuantities()
        {
            if(Recipe != null)
            {
                Ingredients.Clear();
                foreach (var item in Recipe.GetIngredients())
                {
                    item.Quantity = (item.Quantity * sum) / 1000;
                    Ingredients.Add(item);
                }
            }
        }

        private async void OnPrint()
        {
            if (Product == null)
            {
                OnSave();
            }

            PdfDocument pdf = new PdfDocument();
            PdfPage pdfPage = pdf.AddPage();
            pdfPage.Width = 62;
            pdfPage.Height = 30;
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 10, XFontStyle.Bold);
            graph.DrawString("DUPA", font, XBrushes.Black, 20, 20);
            var filename = "test.pdf";
            pdf.Save(Path.Combine("/storage/emulated/0/MintIceApp/", filename));

            CrossToastPopUp.Current.ShowToastMessage("Drukowanie...");
            IBrotherService service = DependencyService.Get<IBrotherService>();
            string result = service.PrintLabelAsync(product.Name);

        }
        private async void OnSave()
        {
            if (Product == null)
            {
                Product = new Product();
            }
            Product.Name = RecipeName;
            Product.Quantity = Sum;
            Product.RecipeId = Recipe.Id;
            ProductRepository.Insert(Product);
            CrossToastPopUp.Current.ShowToastMessage("Zapisano produkt");


            //// This will pop the current page off the navigation stack
            //await Shell.Current.GoToAsync("..");
        }


    }
}

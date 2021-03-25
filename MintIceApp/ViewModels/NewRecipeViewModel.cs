using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using MintIceApp.Models;
using MintIceApp.Repositories;
using MintIceApp.Views;
using Xamarin.Forms;

namespace MintIceApp.ViewModels
{
    public class NewRecipeViewModel : BaseViewModel
    {
        private string recipeName;
        private string ingredientName;
        private string ingredientQuantity;
        private ObservableCollection<Ingredient> ingredients;

        public NewRecipeViewModel()
        {
            Ingredients = new ObservableCollection<Ingredient>();
            SaveCommand = new Command(OnSave, ValidateSave);
            AddCommand = new Command(AddIngredient, ValidateAdd);
            CancelCommand = new Command(OnCancel);
            IngredientQuantity = "0.000";
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            this.PropertyChanged +=
                (_, __) => AddCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(recipeName);
        }
        private bool ValidateAdd()
        {
            return !String.IsNullOrWhiteSpace(ingredientName) && !String.IsNullOrWhiteSpace(ingredientQuantity);
        }

        public string RecipeName
        {
            get => recipeName;
            set => SetProperty(ref recipeName, value);
        }
        public string IngredientName
        {
            get => ingredientName;
            set => SetProperty(ref ingredientName, value);
        }
        public string IngredientQuantity
        {
            get => ingredientQuantity;
            set => SetProperty(ref ingredientQuantity, value);
        }

        public ObservableCollection<Ingredient> Ingredients
        {
            get => ingredients;
            set => SetProperty(ref ingredients, value);
        }

        public Command SaveCommand { get; }
        public Command AddCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        private void AddIngredient()
        {
            Ingredient i = new Ingredient()
            {
                Name = IngredientName,
                Quantity = Decimal.Parse(IngredientQuantity.Replace('.',','))
            };
            Ingredients.Add(i);
            IngredientName = "";
            IngredientQuantity = "0.000";
            Debug.WriteLine(i.Name);
        }


        private async void OnSave()
        {
            Recipe newItem = new Recipe()
            {
                Name = RecipeName,
            };
            RecipeRepository.Insert(newItem);
            foreach (Ingredient ingredient in Ingredients)
            {
                ingredient.RecipeId = newItem.Id;
                IngredientRepository.Insert(ingredient);
            }

            //await DataStore.AddItemAsync(newItem);

            //// This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}

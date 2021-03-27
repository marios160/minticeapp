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
        private string recipeNote;
        private int sum;
        private string ingredientName;
        private int ingredientQuantity;
        private ObservableCollection<Ingredient> ingredients;
        public Command SaveCommand { get; }
        public Command AddCommand { get; }
        public Command EditCommand { get; }
        public Command RemoveCommand { get; }

        public NewRecipeViewModel()
        {
            Ingredients = new ObservableCollection<Ingredient>();
            SaveCommand = new Command(OnSave, ValidateSave);
            AddCommand = new Command(AddIngredient, ValidateAdd);
            EditCommand = new Command(EditIngredient);
            RemoveCommand = new Command<Ingredient>((ingredient) => RemoveIngredient(ingredient));
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
            this.PropertyChanged += (_, __) => AddCommand.ChangeCanExecute();
            this.PropertyChanged += (_, __) => EditCommand.ChangeCanExecute();
            this.PropertyChanged += (_, __) => RemoveCommand.ChangeCanExecute();
        }


        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(recipeName);
        }
        private bool ValidateAdd()
        {
            return !String.IsNullOrWhiteSpace(ingredientName);
        }

        public int Sum
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

        public string IngredientName
        {
            get => ingredientName;
            set => SetProperty(ref ingredientName, value);
        }
        public int IngredientQuantity
        {
            get => ingredientQuantity;
            set => SetProperty(ref ingredientQuantity, value);
        }

        public ObservableCollection<Ingredient> Ingredients
        {
            get => ingredients;
            set => SetProperty(ref ingredients, value);
        }



        private void AddIngredient()
        {
            Ingredient i = new Ingredient()
            {
                Name = IngredientName,
                Quantity = IngredientQuantity,
            };
            Sum = 0;
            Ingredients.Add(i);
            IngredientName = "";
            IngredientQuantity = 0;
            RefreshSum();
        }

        private void RefreshSum()
        {
            foreach (Ingredient item in Ingredients)
            {
                Sum += item.Quantity;
                Debug.WriteLine(item.Quantity);
            }
        }

        private void EditIngredient()
        {
            throw new NotImplementedException();
        }

        private void RemoveIngredient(Ingredient ingredient)
        {
            Ingredients.Remove(ingredient);
            RefreshSum();
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


            //// This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using MintIceApp.Models;
using MintIceApp.Repositories;
using MintIceApp.Views;
using Plugin.Toast;
using Xamarin.Forms;

namespace MintIceApp.ViewModels
{
    [QueryProperty(nameof(RecipeId), nameof(RecipeId))]
    public class NewRecipeViewModel : BaseViewModel
    {
        private string recipeName;
        private string recipeNote;
        private decimal sum;
        private int ingredientId;
        private string ingredientName;
        private decimal ingredientQuantity;
        private ObservableCollection<Ingredient> ingredients;
        private string recipeId;
        private Recipe recipe;
        private Ingredient ingredient;

        public Command SaveCommand { get; }
        public Command AddCommand { get; }
        public Command RemoveCommand { get; }

     
        public NewRecipeViewModel()
        {

            Title = "Dodaj recepturę";
            Ingredients = new ObservableCollection<Ingredient>();
            SaveCommand = new Command(OnSave, ValidateSave);
            AddCommand = new Command(AddIngredient, ValidateAdd);
            RemoveCommand = new Command<Ingredient>((ingredient) => RemoveIngredient(ingredient));
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
            this.PropertyChanged += (_, __) => AddCommand.ChangeCanExecute();
            this.PropertyChanged += (_, __) => RemoveCommand.ChangeCanExecute();

            
            RefreshSum();
        }


        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(recipeName);
        }
        private bool ValidateAdd()
        {
            return !String.IsNullOrWhiteSpace(ingredientName);
        }

        public string RecipeId
        {
            get { return recipeId; }
            set {
                recipeId = value;
                Recipe = RecipeRepository.FindOneById(Convert.ToInt32(value)).Result;
                RecipeName = Recipe.Name;
                RecipeNote = Recipe.Note;
                foreach (var item in Recipe.GetIngredients())
                {
                    Ingredients.Add(item);
                }
                RefreshSum();
                Title = "Edytuj recepturę";
                
            }
        }
        public Recipe Recipe
        {
            get => recipe;
            set => SetProperty(ref recipe, value);
        }
        public Ingredient Ingredient
        {
            get => ingredient;
            set => SetProperty(ref ingredient, value);
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
        public int IngredientId
        {
            get => ingredientId;
            set => SetProperty(ref ingredientId, value);
        }
        public string IngredientName
        {
            get => ingredientName;
            set => SetProperty(ref ingredientName, value);
        }
        public decimal IngredientQuantity
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
            Ingredients.Add(i);
            IngredientName = "";
            IngredientQuantity = 0;
            RefreshSum();
        }

        private void RefreshSum()
        {
            Sum = 0;
            foreach (Ingredient item in Ingredients)
            {
                Sum += item.Quantity;
            }
        }

        public void EditIngredient()
        {
            Ingredient.Name = IngredientName;
            Ingredient.Quantity = IngredientQuantity;
            IngredientRepository.Insert(Ingredient);
            Ingredients.Clear();
            foreach (var item in Recipe.GetIngredients())
            {
                Ingredients.Add(item);
            }
            IngredientName = "";
            IngredientQuantity = 0;
            RefreshSum();
        }

        private void RemoveIngredient(Ingredient ingredient)
        {
            IngredientRepository.Delete(ingredient);
            Ingredients.Remove(ingredient);
            RefreshSum();
        }

        private async void OnSave()
        {
            if (Recipe == null)
            {
                Recipe = new Recipe();
            }

            Recipe.Name = RecipeName;
            Recipe.Note = RecipeNote;
            RecipeRepository.Insert(Recipe);
            foreach (Ingredient ingredient in Ingredients)
            {
                ingredient.RecipeId = Recipe.Id;
                Console.WriteLine(ingredient.Quantity);
                IngredientRepository.Insert(ingredient);
                Console.WriteLine(ingredient.Quantity);
            }

            CrossToastPopUp.Current.ShowToastMessage("Zapisano recepturę");


            //// This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}

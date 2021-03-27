using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MintIceApp.Models;
using MintIceApp.Repositories;
using Xamarin.Forms;

namespace MintIceApp.ViewModels
{
    [QueryProperty(nameof(RecipeId), nameof(RecipeId))]

    public class ItemDetailViewModel : BaseViewModel
    {
        private string recipeName;
        private string recipeNote;
        private int sum;
        private ObservableCollection<Ingredient> ingredients;
        private ObservableCollection<Ingredient> BaseIngredients;
        private string recipeId;
        private Recipe recipe;

        public Command SaveCommand { get; }

        public ItemDetailViewModel()
        {
            SaveCommand = new Command(OnSave);
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
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
                    BaseIngredients.Add(item);
                }

            }
        }
        public Recipe Recipe
        {
            get => recipe;
            set => SetProperty(ref recipe, value);
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

        public ObservableCollection<Ingredient> Ingredients
        {
            get => ingredients;
            set => SetProperty(ref ingredients, value);
        }

        public void RefreshQuantities()
        {
            for (int i = 0; i < Ingredients.Count; i++)
            {
                Ingredients[i].Quantity = BaseIngredients[i].Quantity * sum / 1000;
            }
            
        }

        private async void OnSave()
        {
            Product product = new Product();
            product.Name = RecipeName;
            product.Quantity = Sum;
            product.RecipeId = Recipe.Id;
            ProductRepository.Insert(product);


            //// This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }


    }
}

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MintIceApp.Models;
using MintIceApp.Views;
using MintIceApp.Repositories;
using System.Collections.Generic;
using Plugin.Toast;

namespace MintIceApp.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        private string image;
        private ObservableCollection<Recipe> items;

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Recipe> EditCommand { get; }
        public Command<Recipe> RemoveCommand { get; }
        public Command<Recipe> FavouriteCommand { get; }
        public Command<Recipe> ItemTapped { get; }

        public RecipesViewModel()
        {
            Title = "Receptury";
            Items = new ObservableCollection<Recipe>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            EditCommand = new Command<Recipe>((recipe) => EditRecipe(recipe));
            RemoveCommand = new Command<Recipe>((recipe) => RemoveRecipe(recipe));
            FavouriteCommand = new Command<Recipe>((recipe) => FavouriteRecipe(recipe));
            ItemTapped = new Command<Recipe>((recipe) => OnItemSelected(recipe));

            AddItemCommand = new Command(OnAddItem);
        }

        private void FavouriteRecipe(Recipe recipe)
        {
            int i = Items.IndexOf(recipe);
            Items[i].Favourite = !Items[i].Favourite;
            RecipeRepository.Insert(Items[i]);
            if (Items[i].Favourite)
            {
                Image = "star_solid.png";
                CrossToastPopUp.Current.ShowToastMessage("Dodano do ulubionych");
            }
            else
            {
                Image = "star_regular.png";
                CrossToastPopUp.Current.ShowToastMessage("Usunięto z ulubionych");
            }
            OnAppearing();
        }

        private void RemoveRecipe(Recipe recipe)
        {
            Items.Remove(recipe);
            RecipeRepository.Delete(recipe);
            CrossToastPopUp.Current.ShowToastMessage("Usunięto recepturę");

        }

        private async void EditRecipe(Recipe recipe)
        {
            await Shell.Current.GoToAsync($"{nameof(NewRecipePage)}?RecipeId={recipe.Id}");
        }

        public ObservableCollection<Recipe> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                foreach (var item in RecipeRepository.FindAll().Result)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        
        public string Image
        {
            get => image;
            set => SetProperty(ref image, value);   
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewRecipePage));
        }

        async void OnItemSelected(Recipe item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?RecipeId={item.Id}");
        }
    }
}
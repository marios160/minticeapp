using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MintIceApp.Models;
using MintIceApp.Views;
using MintIceApp.Repositories;
using Plugin.Toast;

namespace MintIceApp.ViewModels
{
    [QueryProperty(nameof(DateString), nameof(DateString))]
    public class SummaryViewModel : BaseViewModel
    {

        public ObservableCollection<Product> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command RemoveCommand { get; }
        public Command<Product> ItemTapped { get; }


        private DateTime date;
        private string dateString;
        public SummaryViewModel()
        {
            Title = "Podsumowanie dnia";
            Items = new ObservableCollection<Product>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Product>((product) => OnItemSelected(product));
            RemoveCommand = new Command<Product>((product) => RemoveProduct(product));
            this.PropertyChanged += (_, __) => RemoveCommand.ChangeCanExecute();


            AddItemCommand = new Command(OnAddItem);

            //Date = DateTime.Now.ToString("D");
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await ProductRepository.FindAllByCreatedAt(Date);
                foreach (var item in items)
                {
                    item.Quantity = item.Quantity / 1000;
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

        private void RemoveProduct(Product product)
        {
            Items.Remove(product);
            ProductRepository.Delete(product);
            CrossToastPopUp.Current.ShowToastMessage("Usunięto produkt");
        }

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public string DateString
        {
            get { return dateString; }
            set {
                dateString = value;
                Date = Convert.ToDateTime(value);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        async void OnItemSelected(Product item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?ProductId={item.Id}");
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewRecipePage));
        }

        
    }
}
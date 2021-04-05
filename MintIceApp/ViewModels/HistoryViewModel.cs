using MintIceApp.Models;
using MintIceApp.Repositories;
using MintIceApp.Views;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MintIceApp.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        private bool filtering;
        private DateTime dateFrom;
        private DateTime dateTo;
        private ObservableCollection<HistoryItem> items;
        public Command FilteringCommand { get; }
        public Command LoadItemsCommand { get; }
        public Command<HistoryItem> ItemTapped { get; }

        public HistoryViewModel()
        {
            Title = "Historia produkcji";
            FilteringCommand = new Command(async () => await FilteringStartAsync());
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<HistoryItem>((item) => OnItemSelected(item));
            this.PropertyChanged += (_, __) => FilteringCommand.ChangeCanExecute();
            Filtering = false;
            Items = new ObservableCollection<HistoryItem>();
            dateTo = DateTime.Now;
            dateFrom = DateTime.Now;

        }
        public ObservableCollection<HistoryItem> Items
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
                List<Product> products;
                if (Filtering)
                    products = await ProductRepository.FindAllByFiltering(dateFrom,dateTo);
                else
                    products = await ProductRepository.FindAll();
                products.Reverse();
                HistoryItem tmp = new HistoryItem();
                foreach (var item in products)
                {
                    if(item.CreatedAt.Date != tmp.date)
                    {
                        if(tmp.count > 0)
                        {
                            tmp.quantity = tmp.quantity / 1000;
                            Items.Add(tmp);
                        }
                        tmp = new HistoryItem();
                        tmp.date = item.CreatedAt.Date;
                    }
                    tmp.count++;
                    tmp.quantity += item.Quantity;
                }
                if (tmp.count > 0)
                {
                    tmp.quantity = tmp.quantity / 1000;
                    Items.Add(tmp);
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
        async void OnItemSelected(HistoryItem item)
        {
            if (item == null)
                return;
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(SummaryPage)}?DateString={item.date.ToString("d")}");
        }
        private async Task FilteringStartAsync()
        {
            Filtering = !Filtering;
            if(Filtering)
                CrossToastPopUp.Current.ShowToastMessage("Filtrowanie włączone");
            else
                CrossToastPopUp.Current.ShowToastMessage("Filtrowanie wyłączone");
            IsBusy = true;

        }

        public bool Filtering
        {
            get => filtering;
            set => SetProperty(ref filtering, value);
        }
        public DateTime DateFrom
        {
            get => dateFrom;
            set => SetProperty(ref dateFrom, value);
        }
        public DateTime DateTo
        {
            get => dateTo;
            set => SetProperty(ref dateTo, value);
        }
    }

    public class HistoryItem
    {

        public DateTime date { get; set; }
        public int count { get; set; }
        public decimal quantity { get; set; }

        public HistoryItem()
        {
            this.count = 0;
            this.quantity = 0;
        }
    }
}

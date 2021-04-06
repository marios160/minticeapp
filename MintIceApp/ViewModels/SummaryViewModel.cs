using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using PdfSharp;


using Xamarin.Forms;

using MintIceApp.Models;
using MintIceApp.Views;
using MintIceApp.Repositories;
using Plugin.Toast;
using System.IO;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Fonts;
using PdfSharpCore.Utils;
using PdfSharp.Xamarin.Forms;

namespace MintIceApp.ViewModels
{
    [QueryProperty(nameof(DateString), nameof(DateString))]
    public class SummaryViewModel : BaseViewModel
    {

        public ObservableCollection<Product> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command GeneratePDFCommand { get; }
        public Command RemoveCommand { get; }
        public Command<Product> ItemTapped { get; }
        private View view;

        private DateTime date;
        private string dateString;
        public SummaryViewModel(View view)
        {
            Title = "Podsumowanie dnia";
            Items = new ObservableCollection<Product>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Product>((product) => OnItemSelected(product));
            RemoveCommand = new Command<Product>((product) => RemoveProduct(product));
            this.PropertyChanged += (_, __) => RemoveCommand.ChangeCanExecute();


            GeneratePDFCommand = new Command(GeneratePDF);

            Date = DateTime.Now;
            this.view = view;
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

        private async void GeneratePDF(object obj)
        {
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "My First PDF";
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 24, XFontStyle.Bold);
            graph.DrawString(Date.ToString("D"), font, XBrushes.Black, 30, 60);
            double y = 100;
            font = new XFont("Verdana", 14, XFontStyle.Regular);
            decimal sum = 0;
            foreach (var item in Items)
            {
                graph.DrawString(item.Name, font, XBrushes.Black, 60, y);
                graph.DrawString(item.Quantity + " kg", font, XBrushes.Black, 300, y);
                y += 30;
                sum += item.Quantity;
                if(y > 700)
                {
                    graph = XGraphics.FromPdfPage(pdf.AddPage());
                    y = 100;
                }                
            }
            font = new XFont("Verdana", 24, XFontStyle.Bold);
            graph.DrawString("Suma: " + sum + " kg", font, XBrushes.Black, 30, y+20);

            var filename = Date.ToString("yyyy-MM-dd_HH-mm-ss") + ".pdf";
            pdf.Save(Path.Combine("/storage/emulated/0/MintIceApp/Summaries", filename));
            CrossToastPopUp.Current.ShowToastMessage("Wygenerowano podsumowanie PDF (Pamięć wewnętrzna/MintIceApp/Summaries/" + filename + ")");

        }



    }
}
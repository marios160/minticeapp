using MintIceApp.Models;
using MintIceApp.Repositories;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MintIceApp.ViewModels
{
    class MonthSummaryViewModel : BaseViewModel
    {
        private List<string> years;
        private List<string> months;
        private string selectedMonth;
        private string selectedYear;
        public Command GeneratePDFCommand { get; }

        public MonthSummaryViewModel()
        {
            Title = "Podsumowanie miesięczne";
            Years = new List<string>();
            Months = new List<string>();
            for (int i = DateTime.Now.Year; i >= 2021 ; i--)
            {
                Years.Add(i.ToString());
            }
            Months.Add("Styczeń");
            Months.Add("Luty");
            Months.Add("Marzec");
            Months.Add("Kwiecień");
            Months.Add("Maj");
            Months.Add("Czerwiec");
            Months.Add("Lipiec");
            Months.Add("Sierpień");
            Months.Add("Wrzesień");
            Months.Add("Październik");
            Months.Add("Listopad");
            Months.Add("Grudzień");
            SelectedYear = DateTime.Now.Year.ToString();
            SelectedMonth = DateTime.Now.ToString("MMMM").Substring(0,1).ToUpper() + DateTime.Now.ToString("MMMM").Substring(1).ToLower();
            GeneratePDFCommand = new Command(GeneratePDF);
        }

        private async void GeneratePDF(object obj)
        {
            int year = Convert.ToInt32(SelectedYear);
            int month = DateTime.ParseExact(SelectedMonth.ToLower(), "MMMM", CultureInfo.CurrentCulture).Month;
            DateTime dateFrom = new DateTime(year,month,1,0,0,0); 
            DateTime dateTo = new DateTime(year,month, DateTime.DaysInMonth(year, month),23,59,59);
            var products = await ProductRepository.FindAllByFiltering(dateFrom, dateTo);
            if (products.Count == 0)
            {
                CrossToastPopUp.Current.ShowToastMessage("Brak wyprodukowanych receptur w danym miesiącu");
                return;
            }
            List<Product> list = new List<Product>();
            foreach (var item in products)
            {
                if (list.Where(p => p.Name == item.Name).Any())
                {
                    var pr = list.Where(p => p.Name == item.Name).FirstOrDefault();
                    pr.Quantity = pr.Quantity + item.Quantity;
                } 
                else
                {
                    list.Add(new Product(item.Quantity, item.Name));
                }
            }

            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "Podsumowanie Miesięczne";
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 24, XFontStyle.Bold);
            graph.DrawString("Podsumowanie miesięczne - " + SelectedMonth + " " + SelectedYear, font, XBrushes.Black, 30, 60);
            double y = 100;
            font = new XFont("Verdana", 14, XFontStyle.Regular);
            decimal sum = 0;
            foreach (var item in list)
            {
                graph.DrawString(item.Name, font, XBrushes.Black, 60, y);
                graph.DrawString((item.Quantity / 1000) + " kg", font, XBrushes.Black, 300, y);
                y += 30;
                sum += item.Quantity;
                if (y > 700)
                {
                    graph = XGraphics.FromPdfPage(pdf.AddPage());
                    y = 100;
                }
            }
            font = new XFont("Verdana", 24, XFontStyle.Bold);
            graph.DrawString("Suma: " + (sum / 1000) + " kg", font, XBrushes.Black, 30, y + 20);

            var filename = selectedYear + "-" + selectedMonth + "_" + DateTime.Now.ToString("HH-mm-ss") + ".pdf";
            pdf.Save(Path.Combine("/storage/emulated/0/MintIceApp/Summaries", filename));
            CrossToastPopUp.Current.ShowToastMessage("Wygenerowano podsumowanie miesiąca (Pamięć wewnętrzna/MintIceApp/Summaries/" + filename + ")");
        }

        public List<string> Years
        {
            get => years;
            set => SetProperty(ref years, value);
        }

        public List<string> Months
        {
            get => months;
            set => SetProperty(ref months, value);
        }
        public string SelectedMonth
        {
            get => selectedMonth;
            set => SetProperty(ref selectedMonth, value);
        }

        public string SelectedYear
        {
            get => selectedYear;
            set => SetProperty(ref selectedYear, value);
        }
    }
}

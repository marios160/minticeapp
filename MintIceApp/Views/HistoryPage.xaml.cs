using MintIceApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MintIceApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        HistoryViewModel vm;
        public HistoryPage()
        {
            InitializeComponent();
            vm = new HistoryViewModel();
            BindingContext = vm;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.OnAppearing();
        }
 

        private void dateTo_DateSelected(object sender, DateChangedEventArgs e)
        {
            vm.OnAppearing();
        }

        private void dateFrom_DateSelected(object sender, DateChangedEventArgs e)
        {
            vm.OnAppearing();
        }
    }
}
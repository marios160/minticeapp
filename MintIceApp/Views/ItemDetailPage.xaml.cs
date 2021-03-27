using System.ComponentModel;
using Xamarin.Forms;
using MintIceApp.ViewModels;

namespace MintIceApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel vm;
        public ItemDetailPage()
        {
            InitializeComponent();
            vm = new ItemDetailViewModel();
            BindingContext = vm;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            vm.RefreshQuantities();
        }
    }
}